using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Scheduler.Models;

namespace Scheduler {
    public partial class CustomerForm : Form {
        int? customerId;
        Customer? customer;
        MainScreen mainScreen;

        public CustomerForm(MainScreen mainScreen, int? customerId) {
            this.mainScreen = mainScreen;

            InitializeComponent();

            if (customerId != null) {
                this.customerId = customerId;
            }

            initializeFormType();
        }

        private void initializeFormType() {
            if (null == customerId) {
                this.Text = "Create New Customer";
                button_appt.Visible = false;
                button_delete.Visible = false;
            } else {
                this.Text = "View/Edit Customer";
                button_appt.Visible = true;
                button_delete.Visible = true;

                customer = DataAccessLayer.GetCustomerById(customerId.Value);
                if (customer == null) {
                    showError("Customer not found");
                    Close();
                    return;
                }

                Customer c = customer as Customer;
                textBox_name.Text = c.customerName;
                checkBox_active.Checked = c.active;
                textBox_address1.Text = c.address.address;
                textBox_address2.Text = c.address.address2;
                textBox_city.Text = c.address.city.city;
                textBox_country.Text = c.address.city.country.country;
                textBox_postcode.Text = c.address.postalCode;
                textBox_phone.Text = c.address.phone;
            }
        }

        private void showError(string message) {
            MessageBox.Show(
                message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        private void showSuccess(string message) {
            MessageBox.Show(
                message,
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void button_save_Click(object sender, EventArgs e) {
            string name = textBox_name.Text.Trim();
            bool active = checkBox_active.Checked;
            string address1 = textBox_address1.Text.Trim();
            string address2 = textBox_address2.Text.Trim();
            string city = textBox_city.Text.Trim();
            string country = textBox_country.Text.Trim();
            string postcode = textBox_postcode.Text.Trim();
            string phone = textBox_phone.Text.Trim();

            if (!validateInputs(name, address1, address2, city, country, postcode, phone)) {
                return;
            }

            int? countryId = DataAccessLayer.GetCountryId(country);
            if (null == countryId) {
                showError("Something went wrong recording the country.");
                return;
            }

            int? cityId = DataAccessLayer.GetCityId(city, countryId.Value);
            if (null == cityId) {
                showError("Something went wrong recording the city.");
                return;
            }

            int? addressId = DataAccessLayer.GetAddressId(address1, address2, cityId.Value, postcode, phone);
            if (null == addressId) {
                showError("Something went wrong recording the address.");
                return;
            }

            if (customerId != null) {
                bool success = DataAccessLayer.UpdateCustomer(customerId.Value, name, addressId.Value, active);
                if (success) {
                    showSuccess("Customer record updated!");
                    mainScreen.populateCustomersGrid();
                } else {
                    showError("There was an error updating the customer record.");
                }
            } else {
                customerId = DataAccessLayer.CreateOrUpdateCustomer(name, addressId.Value, active);
                if (customerId != null) {
                    showSuccess("Customer record created!");
                    initializeFormType();
                    mainScreen.populateCustomersGrid();
                } else {
                    showError("There was an error creating the customer record.");
                }
            }
        }

        private bool validateInputs(string name, string address1, string address2, string city, string country, string postcode, string phone) {
            if (string.IsNullOrEmpty(name)) {
                showError("Please enter a name.");
                return false;
            }

            if (name.Length > 45) {
                showError("Name may not be more than 45 characters.");
                return false;
            }

            if (string.IsNullOrEmpty(address1)) {
                showError("Please enter an address.");
                return false;
            }

            if (address1.Length > 50 || address2.Length > 50) {
                showError("Address may not be more than 50 characters per line.");
                return false;
            }

            if (string.IsNullOrEmpty(city)) {
                showError("Please enter a city.");
                return false;
            }

            if (city.Length > 50) {
                showError("City may not be more than 50 characters.");
                return false;
            }

            if (string.IsNullOrEmpty(country)) {
                showError("Please enter a country.");
                return false;
            }

            if (country.Length > 50) {
                showError("Country may not be more than 50 characters.");
                return false;
            }

            if (string.IsNullOrEmpty(postcode)) {
                showError("Please enter a postcode.");
                return false;
            }

            if (postcode.Length > 10) {
                showError("Postcode may not be more than 10 characters.");
                return false;
            }

            if (string.IsNullOrEmpty(phone)) {
                showError("Please enter a phone number.");
                return false;
            }

            if (phone.Length > 10) {
                showError("Phone may not be more than 10 characters.");
                return false;
            }

            if (!Regex.IsMatch(phone, @"^[0-9\-]+$")) {
                showError("Invalid phone: only numbers and dashes allowed.");
                return false;
            }

            if (!Regex.IsMatch(postcode, @"^[0-9\-]+$")) {
                showError("Invalid postcode: only numbers and dashes allowed.");
                return false;
            }

            return true;
        }

        private void button_close_Click(object sender, EventArgs e) {
            Close();
        }

        private void button_delete_Click(object sender, EventArgs e) {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to permanently delete this customer and all of their appointments?",
                "Danger",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation
            );
            if (result == DialogResult.Yes) {
                if (DataAccessLayer.HasAppointmentsWithOtherUsers(customerId.Value)) {
                    showError("You cannot delete customers that have appointments with other users. Have the other usres delete their appointments and then try again.");
                    return;
                }
                DataAccessLayer.DeleteCustomer(customerId.Value);
                mainScreen.populateCustomersGrid();
                Close();
                showSuccess("Customer Deleted!");
            }
        }

        private void button_appt_Click(object sender, EventArgs e) {
            AppointmentForm apptForm = new AppointmentForm(mainScreen, null, customerId.Value);
            apptForm.ShowDialog();
        }
    }
}
