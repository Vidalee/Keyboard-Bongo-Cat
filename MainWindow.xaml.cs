using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace BongoCatKeys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Dispatcher dispatcher;

        //State of each images ((not) visible, key (not) pressed)
        bool db = false;
        bool fb = false;
        bool jb = false;
        bool kb = false;

        bool dp = false;
        bool fp = false;
        bool dfp = false;
        bool jp = false;
        bool kp = false;
        bool jkp = false;

        bool ul = false;
        bool ur = false;

        public MainWindow()
        {
            dispatcher = Dispatcher.CurrentDispatcher;

            InitializeComponent();

            Thread kThread = new Thread(Handler);
            kThread.SetApartmentState(ApartmentState.STA);
           
            kThread.Start();
        }

        void Handler()
        {
            //Note: it's not good to do this. But it works :)
            while (true) // :^)
            {
                Thread.Sleep(40);
                if (Keyboard.GetKeyStates(Key.Q).ToString().Contains("Down")) db = true;
                else db = false;
                if (Keyboard.GetKeyStates(Key.S).ToString().Contains("Down")) fb = true;
                else fb = false;
                if (Keyboard.GetKeyStates(Key.L).ToString().Contains("Down")) jb = true;
                else jb = false;
                if (Keyboard.GetKeyStates(Key.M).ToString().Contains("Down")) kb = true;
                else kb = false;

                PerformLogic();

                dispatcher.Invoke(new Action(() => { UpdateGraph(); }));
            }
            
        }

        //Hands and lights logic
        void PerformLogic()
        {
            if (db && fb)
            {
                dfp = true;
                dp = false;
                fp = false;
            }
            else
            {
                dfp = false;
                if (db) dp = true;
                else dp = false;
                if (fb) fp = true;
                else fp = false;

            }
            if (jb && kb)
            {
                jkp = true;
                jp = false;
                kp = false;
            }
            else
            {
                jkp = false;
                if (jb) kp = true;
                else kp = false;
                if (kb) jp = true;
                else jp = false;
            }
            if (!db && !fb) ul = true;
            else ul = false;
            if (!jb && !kb) ur = true;
            else ur = false;
        }

        //Update the images on the window (make them (in)visible)
        void UpdateGraph()
        {
            if (db) d.Visibility = Visibility.Visible;
            else d.Visibility = Visibility.Hidden;
            if (fb) f.Visibility = Visibility.Visible;
            else f.Visibility = Visibility.Hidden;
            if (jb) j.Visibility = Visibility.Visible;
            else j.Visibility = Visibility.Hidden;
            if (kb) k.Visibility = Visibility.Visible;
            else k.Visibility = Visibility.Hidden;

            if (dp) pat_down_l_d.Visibility = Visibility.Visible;
            else pat_down_l_d.Visibility = Visibility.Hidden;
            if (fp) pat_down_l_f.Visibility = Visibility.Visible;
            else pat_down_l_f.Visibility = Visibility.Hidden;
            if (dfp) pat_down_l_df.Visibility = Visibility.Visible;
            else pat_down_l_df.Visibility = Visibility.Hidden;

            if (jp) pat_down_r_j.Visibility = Visibility.Visible;
            else pat_down_r_j.Visibility = Visibility.Hidden;
            if (kp) pat_down_r_k.Visibility = Visibility.Visible;
            else pat_down_r_k.Visibility = Visibility.Hidden;
            if (jkp) pat_down_r_jk.Visibility = Visibility.Visible;
            else pat_down_r_jk.Visibility = Visibility.Hidden;

            if (ul) left_up.Visibility = Visibility.Visible;
            else left_up.Visibility = Visibility.Hidden;
            if (ur) right_up.Visibility = Visibility.Visible;
            else right_up.Visibility = Visibility.Hidden;
        }
    }
}
