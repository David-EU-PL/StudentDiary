﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace StudentDiary
{
    public partial class Main : Form
    {
        private string _filePath = 
            Path.Combine(Environment.CurrentDirectory, "students.txt");

        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);
        public Main()
        {
            InitializeComponent();
            RefreshDiary();
            SetColumnsHeader();

            var student1 = new Student();
            student1.FirstName = "1";
            student1.Id = 1;

            var person = new Person();
            person.Id = 2;

            person = student1;
            MessageBox.Show(person.Id.ToString());

        }

        private void RefreshDiary()
        {
            var students = _fileHelper.DeserializeFromFile();
            dgvDiary.DataSource = students;
        }

   

        private void SetColumnsHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Numer";
            dgvDiary.Columns[1].HeaderText = "Imię";
            dgvDiary.Columns[2].HeaderText = "Nazwisko";
            dgvDiary.Columns[3].HeaderText = "Matematyka";
            dgvDiary.Columns[4].HeaderText = "Technologia";
            dgvDiary.Columns[5].HeaderText = "Chemia";
            dgvDiary.Columns[6].HeaderText = "Język polski";
            dgvDiary.Columns[7].HeaderText = "Język obcy";
            dgvDiary.Columns[8].HeaderText = "Uwagi";
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudents();
            addEditStudent.ShowDialog();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego dane chcesz edytować.");
                return;
            }

            var addEditStudent = new AddEditStudents(Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
            addEditStudent.ShowDialog();

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego dane chcesz usunąć.");
                return;
            }
            var selectedStudent = dgvDiary.SelectedRows[0];

            var confirmDelete =
                MessageBox.Show($"Czy na pewno chcesz usunąć {(selectedStudent.Cells[1].Value.ToString() + " " + selectedStudent.Cells[2].Value.ToString()).Trim()}?","Usuwanie ucznia", MessageBoxButtons.OKCancel);

            if (confirmDelete == DialogResult.OK)
            {
                DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value));
                RefreshDiary(); 
            }
        }

        private void DeleteStudent(int id)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.Id == id);
            _fileHelper.SerializeToFile(students);
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }
    }
}
