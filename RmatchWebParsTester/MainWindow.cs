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
        [UI] private Entry user = null;
        [UI] private Entry password = null;
        [UI] private CheckButton authorization = null;
        
        private int _counter;

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
            password.Visible = false;
            user.Visible = false;
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);

            authorization.Clicked += authorization_Clicked;
            DeleteEvent += Window_DeleteEvent;
            button.Clicked += Button_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private async void authorization_Clicked(object sender, EventArgs a)
        {
            if (authorization.Active==true)
            {
                password.Visible = true;
                user.Visible = true;
            }
            else
            {
                user.Visible = false;
                password.Visible = false;
            }
            Console.WriteLine(authorization.Active);
        }





        private async void Button_Clicked(object sender, EventArgs a)
        {
            
            await Task.Run(()  =>
            {


                if (authorization.Active==false)
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

                            if (res != ""&res.Length<500)
                            {
                                textV.Buffer.Text = res; 
                            }
                            else
                            {
                                textV.Buffer.Text = "NON";
                            }
                

                        }
                    
                    
                    });
                }
                else
                { string Durl= AuthorizationWEB.Download(url.Text,user.Text,password.Text);
                    Gtk.Application.Invoke(delegate
                    {  
                        textV.Buffer.Text = Durl;
                        if (Durl=="Error web")
                        {
                            
                        }
                        else
                        {
                            string res = default;
                            try
                            {
                                res = Pars.match(Durl, Rmatch.Text);
                            }
                            catch (Exception e)
                            {
                                textV.Buffer.Text = "NON";
                            }
                         
                           
                            
                            if (res != "")
                            {
                                textV.Buffer.Text = res; 
                            }
                            else
                            {
                                textV.Buffer.Text = "NON";
                            }
                            
                        }
                        
                        
                    });
                }
                
               
            });
        }
    }
}