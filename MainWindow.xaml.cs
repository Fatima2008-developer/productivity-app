using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Productivity_App.Tracker;
using System.IO;
namespace Productivity_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<TaskItem> Tasks;
        private readonly string file= "tasks.json";



        public MainWindow()
        {
            InitializeComponent();
            LoadTasks();
            TaskList.ItemsSource = Tasks;


        }
        // ✅ Load tasks from JSON
        private void LoadTasks()
        {
            if (File.Exists(file))
            {
                string json = File.ReadAllText(file);
                Tasks = JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(json);
            }
            else
            {
                Tasks = new ObservableCollection<TaskItem>();
            }

        }

        // ✅ Save tasks to JSON
        private void SaveTasks()
        {
            string json = JsonSerializer.Serialize(Tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(file, json);


        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskItem task)
            {
                string newTitle = Microsoft.VisualBasic.Interaction.InputBox("Edit task:", "Edit Task", task.Title);
                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    task.Title = newTitle;
                    SaveTasks();
                    TaskList.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Select a task to edit.");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (TaskList.SelectedItem is TaskItem task)
            {
                Tasks.Remove(task);
                SaveTasks();
            }
            else
            {
                MessageBox.Show("Select a task to delete.");
            }



        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SaveTasks();
            MessageBox.Show("Tasks saved!");

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string title = Microsoft.VisualBasic.Interaction.InputBox("Enter task title:", "Add Task");
            if (!string.IsNullOrWhiteSpace(title))
            {
                Tasks.Add(new TaskItem { Title = title, CreatedAt = DateTime.Now.ToString("g"), IsCompleted = false });
                SaveTasks();
            }

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchBox.Text.ToLower();
            TaskList.ItemsSource = string.IsNullOrEmpty(keyword)
                ? Tasks
                : new ObservableCollection<TaskItem>(Tasks.Where(t => t.Title.ToLower().Contains(keyword)));

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SaveTasks();

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SaveTasks();

        }

        private void themetoggle_Checked(object sender, RoutedEventArgs e)
        {
            Theme.ChangeTheme(new Uri("/Dictionary2.Xaml", UriKind.Relative));

        }

        private void themetoggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Theme.ChangeTheme(new Uri("/Dictionary1.Xaml", UriKind.Relative));

        }
    }
}