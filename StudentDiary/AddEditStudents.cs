using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace StudentDiary
{
    public partial class AddEditStudents : Form
    {
        private int _studentId;
        private Student _student;

        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);
        public AddEditStudents(int id = 0)
        {
            InitializeComponent();
            _studentId = id;
            GetStudentData();  
            tbFirstName.Select();
        }

        private void GetStudentData()
        {
            if (_studentId != 0)
            {
                Text = "Edytowanie danych ucznia";

                var students = _fileHelper.DeserializeFromFile();
                _student = students.FirstOrDefault(x => x.Id == _studentId);


                if (_student == null)

                    throw new Exception("Brak użytkownika o podanym ID");

                FillTextBoxes();
                
            }
            
        }
       private void FillTextBoxes()
        {
            tbID.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbLastName.Text = _student.LastName;
            tbMath.Text = _student.Math;
            tbChemistry.Text = _student.Chemistry;
            tbTechnology.Text = _student.Technology;
            tbPolishLang.Text = _student.PolishLang;
            tbForeignLang.Text = _student.ForeignLang;
            rtbComments.Text = _student.Comments;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (_studentId != 0)
                students.RemoveAll(x => x.Id == _studentId);
            else
                AssignIdToNewStudent(students);

            AddNewUserToList(students);
            _fileHelper.SerializeToFile(students);
            Close();
        }

        private void AddNewUserToList(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Comments = rtbComments.Text,
                ForeignLang = tbForeignLang.Text,
                Math = tbMath.Text,
                PolishLang = tbPolishLang.Text,
                Technology = tbTechnology.Text,
                Chemistry = tbChemistry.Text,
            };
            students.Add(student);
        }
        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentWithHighestId = students.OrderByDescending(x => x.Id).FirstOrDefault();

            _studentId = studentWithHighestId == null ?
            1 : studentWithHighestId.Id + 1;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lbLastName_Click(object sender, EventArgs e)
        {

        }

        private void AddEditStudents_Load(object sender, EventArgs e)
        {

        }
    }
}
