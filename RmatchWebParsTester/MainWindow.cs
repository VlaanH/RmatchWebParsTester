using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Task = System.Threading.Tasks.Task;

namespace RmatchWebParsTester
{
    class MainWindow : Window
    {
        [UI] private TextView textV = null;
        [UI] private Entry url = null;
        [UI] private Entry Rmatch = null;
        [UI] private Button button = null;

        private int _counter;

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            button.Clicked += Button_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private async void Button_Clicked(object sender, EventArgs a)
        {
            
            await Task.Run(()  =>
            {   
                string Durl = web.Download(url.Text);
            
                Gtk.Application.Invoke(delegate
                {
                    
                    if (Durl=="error")
                    {
                        textV.Buffer.Text = "Error web";
                    }
                    else
                    {
                       string res = Pars.match(Durl, Rmatch.Text);

                       if (res != ""&res.Length<200)
                       {
                           textV.Buffer.Text = res; 
                       }
                       else
                       {
                           textV.Buffer.Text = "NON";
                       }
                

                    }
                    
                    
                });
            });
        }
    }
}