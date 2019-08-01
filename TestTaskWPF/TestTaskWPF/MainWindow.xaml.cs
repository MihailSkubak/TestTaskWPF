using HelixToolkit.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestTaskWPF
{
    public partial class MainWindow : Window
    {
        public Storyboard myStoryboard = new Storyboard();
        public Model3DGroup MyModel;
        readonly ModelVisual3D model_visual = new ModelVisual3D();
        public List<Task> tasksList = new List<Task>();
        bool scene = true;
        int SliderValueMin, SliderValueMax;
        int start_stop = 0;
        public MainWindow()
        {

            InitializeComponent();
            for (int i = 1; i < 4; ++i)
            {
                tasksList.Add(new Task { Name = "Task " + i.ToString() });
            }

            myView.Visibility = Visibility.Hidden;
            loadBtn.Visibility = Visibility.Hidden;
            clearBtn.Visibility = Visibility.Hidden;
            MinZ.Visibility = Visibility.Hidden;
            MaxZ.Visibility = Visibility.Hidden;
            startBtn.Visibility = Visibility.Hidden;
            stopBtn.Visibility = Visibility.Hidden;
            minZ_Number_TB.Visibility = Visibility.Hidden;
            minZ_TB.Visibility = Visibility.Hidden;
            maxZ_Number_TB.Visibility = Visibility.Hidden;
            maxZ_TB.Visibility = Visibility.Hidden;

            myListView.ItemsSource = tasksList;

        }

        private void Button_LOAD(object sender, RoutedEventArgs e)
        {
            if (scene)
            {
                ObjReader CurrentHelixObjReader = new ObjReader();
                OpenFileDialog openFile = new OpenFileDialog();
                if (openFile.ShowDialog() == true)
                {

                    myView.ZoomExtents();



                    MyModel = CurrentHelixObjReader.Read(openFile.FileName);

                    model_visual.Content = MyModel;
                    try
                    {
                        myView.Children.Add(model_visual);
                    }
                    catch { }
                    scene = false;

                }
            }
            else { MessageBox.Show("Очистите сцену кнопкой CLEAR"); }
        }

        private void Button_CLEAR(object sender, RoutedEventArgs e)
        {
            model_visual.Content = null;
            scene = true;
        }


        private void Button_START(object sender, RoutedEventArgs e)
        {
            start_stop = 1;
            FigureRotation();

        }
        private async void FigureRotation()
        {
            int? sideRotation = null;
            while (start_stop == 1)
            {
                NameScope scope = new NameScope();
                FrameworkContentElement element = new FrameworkContentElement();
                NameScope.SetNameScope(element, scope);
                //myView.Children.Add(model_visual);
                myView.ZoomExtents();

                AxisAngleRotation3D rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);
                RotateTransform3D myRotateTransform3D = new RotateTransform3D(rotation, new Point3D(0, 0, 0));
                try
                {
                    MyModel.Transform = myRotateTransform3D;
                }
                catch { }
                element.RegisterName("rotation", rotation);

                // Create two DoubleAnimations and set their properties.
                DoubleAnimation animation = new DoubleAnimation();

                if (sideRotation == 0)
                {
                    animation.From = SliderValueMin;
                    animation.To = SliderValueMax;
                    sideRotation = 1;
                }
                else
                {
                    animation.From = SliderValueMax;
                    animation.To = SliderValueMin;
                    sideRotation = 0;
                }
                animation.Duration = TimeSpan.FromSeconds(2);

                Storyboard.SetTargetProperty(animation, new PropertyPath("Angle"));
                Storyboard.SetTargetName(animation, "rotation");
                myStoryboard.Children.Add(animation);

                myStoryboard.Duration = TimeSpan.FromSeconds(2);
                // Make the Storyboard a resource.
                try
                {
                    this.Resources.Add("unique_id1", myStoryboard);
                }
                catch { }
                myStoryboard.Begin(element, HandoffBehavior.Compose);
                await System.Threading.Tasks.Task.Delay(2000);
            }
        }

        private void Button_STOP(object sender, RoutedEventArgs e)
        {
            start_stop = 0;
        }

        private void MinZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderValueMin = (int)MinZ.Value;
        }

        private void MaxZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderValueMax = (int)MaxZ.Value;
        }

        private void MyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myListView.SelectedItem == tasksList.Where(x => x.Name == "Task 1").First())
            {
                myView.Visibility = Visibility.Visible;
                loadBtn.Visibility = Visibility.Visible;
                clearBtn.Visibility = Visibility.Visible;
                MinZ.Visibility = Visibility.Hidden;
                MaxZ.Visibility = Visibility.Hidden;
                startBtn.Visibility = Visibility.Hidden;
                stopBtn.Visibility = Visibility.Hidden;
                minZ_Number_TB.Visibility = Visibility.Hidden;
                minZ_TB.Visibility = Visibility.Hidden;
                maxZ_Number_TB.Visibility = Visibility.Hidden;
                maxZ_TB.Visibility = Visibility.Hidden;
            }
            if (myListView.SelectedItem == tasksList.Where(x => x.Name == "Task 2").First())
            {
                myView.Visibility = Visibility.Visible;
                loadBtn.Visibility = Visibility.Visible;
                clearBtn.Visibility = Visibility.Visible;
                MinZ.Visibility = Visibility.Hidden;
                MaxZ.Visibility = Visibility.Hidden;
                startBtn.Visibility = Visibility.Hidden;
                stopBtn.Visibility = Visibility.Hidden;
                minZ_Number_TB.Visibility = Visibility.Hidden;
                minZ_TB.Visibility = Visibility.Hidden;
                maxZ_Number_TB.Visibility = Visibility.Hidden;
                maxZ_TB.Visibility = Visibility.Hidden;
            }
            if (myListView.SelectedItem == tasksList.Where(x => x.Name == "Task 3").First())
            {
                myView.Visibility = Visibility.Visible;
                loadBtn.Visibility = Visibility.Visible;
                clearBtn.Visibility = Visibility.Visible;
                MinZ.Visibility = Visibility.Visible;
                MaxZ.Visibility = Visibility.Visible;
                startBtn.Visibility = Visibility.Visible;
                stopBtn.Visibility = Visibility.Visible;
                minZ_Number_TB.Visibility = Visibility.Visible;
                minZ_TB.Visibility = Visibility.Visible;
                maxZ_Number_TB.Visibility = Visibility.Visible;
                maxZ_TB.Visibility = Visibility.Visible;
            }
        }
    }
    public class Task
    {
        public string Name { get; set; }
    }
}
