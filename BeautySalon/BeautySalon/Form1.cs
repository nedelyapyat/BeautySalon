using BeautySalon.Models;
using BeautySalon.Repositories.BeautySalon.Repositories;
using BeautySalon.Repositories;
using BeautySalon.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BeautySalon.Models;
using BeautySalon.Repositories;
using BeautySalon.Services;


namespace BeautySalon
{
    public partial class Form1 : Form
    {
        private readonly AppointmentService _appointmentService;
        private readonly MasterService _masterService;
        private readonly ClientService _clientService;
        private readonly ServiceService _serviceService;

        public Form1()
        {
            InitializeComponent();

            // Укажите вашу строку подключения к базе данных
            string connectionString = "Data Source=WIN-UCHSIQ48T9V;Initial Catalog=BeautySalonDB;Integrated Security=True";

            // Инициализация сервисов
            _appointmentService = new AppointmentService(
                new AppointmentRepository(connectionString),
                new MasterRepository(connectionString),
                new ClientRepository(connectionString),
                new ServiceRepository(connectionString));

            _masterService = new MasterService(new MasterRepository(connectionString));
            _clientService = new ClientService(new ClientRepository(connectionString));
            _serviceService = new ServiceService(new ServiceRepository(connectionString));

            // Настройка вкладок
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            // Загрузка данных
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Загрузка клиентов
                var clients = _clientService.GetAllClients();
                comboBox1.DataSource = clients;
                comboBox1.DisplayMember = "FullName";
                comboBox1.ValueMember = "ClientId";

                // Загрузка мастеров
                var masters = _masterService.GetAllMasters();
                comboBox2.DataSource = masters;
                comboBox2.DisplayMember = "FullName";
                comboBox2.ValueMember = "MasterId";

                // Загрузка услуг
                var services = _serviceService.GetAllServices();
                comboBox3.DataSource = services;
                comboBox3.DisplayMember = "Name";
                comboBox3.ValueMember = "ServiceId";

                // Настройка DateTimePicker для записи
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm";
                dateTimePicker1.ShowUpDown = true;
                dateTimePicker1.Value = DateTime.Now.AddHours(1);

                // Настройка DateTimePicker для просмотра
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = "dd.MM.yyyy";
                dateTimePicker2.Value = DateTime.Today;

                // Обновление списка записей
                RefreshAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshAppointments()
        {
            try
            {
                var appointments = _appointmentService.GetAppointmentsByDate(dateTimePicker2.Value);
                dataGridView1.DataSource = appointments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении записей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage3) // Вкладка "Мастера"
            {
                try
                {
                    var masters = _masterService.GetAllMasters();
                    dataGridView2.DataSource = masters;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке мастеров: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (tabControl1.SelectedTab == tabPage4) // Вкладка "Услуги"
            {
                try
                {
                    var services = _serviceService.GetAllServices();
                    dataGridView3.DataSource = services;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке услуг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите клиента, мастера и услугу", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var appointment = new Appointment
                {
                    ClientId = (int)comboBox1.SelectedValue,
                    MasterId = (int)comboBox2.SelectedValue,
                    ServiceId = (int)comboBox3.SelectedValue,
                    AppointmentDate = dateTimePicker1.Value,
                    Comment = textBox1.Text,
                    Status = "Запланировано"
                };

                _appointmentService.CreateAppointment(appointment);
                MessageBox.Show("Запись успешно создана!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Очистка поля комментария
                textBox1.Text = string.Empty;

                // Обновление списка записей
                RefreshAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshAppointments();
        }

        private void Form1_Load(object sender, EventArgs e) { }
        private void tabPage1_Click(object sender, EventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e) { }
        private void tabPage3_Click(object sender, EventArgs e) { }
        private void tabPage4_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}