using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WorkingWithDates.Classes;
using WorkingWithDates.Data;
using WorkingWithDates.Models;

namespace WorkingWithDates
{
    public partial class EventsForm : Form
    {
        private readonly BindingSource _bindingSource = new ();

        public EventsForm()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;

            Shown += OnShown;
        }

        /// <summary>
        /// Load data via Entity Framework Core 5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnShown(object sender, EventArgs e)
        {

            _bindingSource.DataSource = await DataOperations.PeopleLocal();
            dataGridView1.DataSource = _bindingSource;

            BirthDateDateTimePicker.DataBindings.Add("Value", _bindingSource, "BirthDate", false, 
                DataSourceUpdateMode.OnPropertyChanged);

            BirthDateDateTimePicker.ValueChanged += BirthDateDateTimePickerOnValueChanged;
        }

        /// <summary>
        /// Update current record's birthdate 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BirthDateDateTimePickerOnValueChanged(object sender, EventArgs e)
        {
            if (DataIsNotAccessible()) return;

            var current = (Person1)_bindingSource.Current;

            current.BirthDate = BirthDateDateTimePicker.Value;

        }

        /// <summary>
        /// Demonstrates accessing the current record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentPersonButton_Click(object sender, EventArgs e)
        {

            if (DataIsNotAccessible()) return;

            var current = (Person1)_bindingSource.Current;

            MessageBox.Show($"{current.FirstName} {current.LastName}\n" +
                            $"{current.BirthDate.Value:yyyy-MM-dd}");

        }

        /// <summary>
        /// If there are modified records, show the id and change, deleted
        /// records are marked for deletion but not shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowChangesButton_Click(object sender, EventArgs e)
        {
            if (_bindingSource is null) return;

            var changes = DataOperations.ShowEventsChangesPlain();
            MessageBox.Show(string.IsNullOrWhiteSpace(changes) ? "No changes detected" : changes);
        }

        /// <summary>
        /// Determine if underlying data is accessible 
        /// </summary>
        /// <returns></returns>
        private bool DataIsNotAccessible() => (_bindingSource is null || _bindingSource.Current is null);
    }
}
