using BacklogConvertData.App.Api;
using BacklogConvertData.App.Handle;
using BacklogConvertData.App.Interface.IHandle;
using BacklogConvertData.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BacklogImportData
{
    public partial class MainForm : Form
    {
        private readonly IApiUrlHandle _apiUrlHandle;

        private readonly ICommonApi _commonApi;

        private readonly IComponentApi _componentApi;

        private readonly IMainHandle _mainHandle;

        public MainForm(
            IApiUrlHandle apiUrlHandle,
            ICommonApi commonApi,
            IComponentApi componentApi,
            IMainHandle mainHandle
            )
        {
            InitializeComponent();
            _apiUrlHandle = apiUrlHandle;
            _commonApi = commonApi;
            _componentApi = componentApi;
            _mainHandle = mainHandle;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await GetUsers();
        }

        #region event
        private void openFileExcel_Click(object sender, EventArgs e)
        {
            openFile.ShowDialog();
            string fileName = openFile.FileName;
            displayFileName.Text = fileName;
        }

        private void searchUser_Click(object sender, EventArgs e)
        {
            if (selectUser.SelectedItem != null)
            {
                int userId = (int)selectUser.SelectedValue;
                _ = GetProjectByUser(userId);
            }
            else
            {
                MessageBox.Show("Please select an user");
            }
        }

        private async void submit_Click(object sender, EventArgs e)
        {
            submit.Enabled = false;

            try
            {
                if (string.IsNullOrWhiteSpace(displayFileName.Text))
                {
                    MessageBox.Show("Please select a file import");
                    return;
                }

                if (File.Exists(displayFileName.Text))
                {
                    string fileExtension = Path.GetExtension(displayFileName.Text);
                    if (fileExtension != ".xlsx" && fileExtension != ".xls")
                    {
                        MessageBox.Show("Import file must be an excel format", displayFileName.Text);
                        return;
                    }
                }

                if (selectProject.SelectedValue == null)
                {
                    MessageBox.Show("Please select a project");
                    return;
                }
                else
                {
                    int projectId = (int)selectProject.SelectedValue;
                    new ResponseHandle(this);

                    await _mainHandle.Process(displayFileName.Text, projectId, (int)selectUser.SelectedValue);
                }
            }
            finally
            {
                submit.Enabled = true;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            clearData.Enabled = false;

            try
            {
                if (selectProject.SelectedValue == null)
                {
                    MessageBox.Show("Please select a project");
                    return;
                }
                else
                {
                    int projectId = (int)selectProject.SelectedValue;

                    var handle = new ClearDataHandle();
                    await handle.Handle(projectId);
                }
            }
            finally
            {
                clearData.Enabled = true;
            }
        }

        private Binding BindingNameHandle(List<User> users)
        {
            Binding binding = new Binding("Text", users, "name");

            binding.Format += (s, e) =>
            {
                if (e.DesiredType == typeof(string))
                {
                    e.Value = " - " + e.Value.ToString();
                }
            };

            return binding;
        }
        #endregion

        #region bussiness
        public async Task GetUsers()
        {
            var users = await _commonApi.List<User>(_apiUrlHandle.UrlHasNoProjectId("users"));

            selectUser.DataSource = users;
            selectUser.DisplayMember = "mailAddress";
            selectUser.ValueMember = "id";

            userName.DataBindings.Add(BindingNameHandle(users));
        }

        private async Task GetProjectByUser(int userId)
        {
            var userProjects = await _componentApi.getUserProjectHandle(userId);

            if (userProjects.Count == 0)
            {
                selectProject.DataSource = null;
            }
            else
            {
                List<TypeIdName> projectsData = userProjects;

                selectProject.DataSource = projectsData;
                selectProject.DisplayMember = "name";
                selectProject.ValueMember = "id";
            }
        }

        public void AppendImportLog(string message)
        {
            if (showImportLog.InvokeRequired)
            {
                showImportLog.Invoke(new Action<string>(AppendImportLog), message);
            }
            else
            {
                showImportLog.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
                showImportLog.ScrollToCaret();
            }
        }
        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void displayFileName_TextChanged_1(object sender, EventArgs e)
        {

        }
        private void displayFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void showImportLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void selectProject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
