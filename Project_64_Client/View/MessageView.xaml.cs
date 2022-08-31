using Project_64_Client.Object;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Project_64_Client.View
{
    public partial class MessageView : UserControl
    {
        public ChatMessage Message
        {
            get { return (ChatMessage)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DependencyProperty MessageProperty =  DependencyProperty.Register("Message", typeof(ChatMessage), typeof(MessageView), new PropertyMetadata(null));
        public MessageView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new System.Media.SoundPlayer(new MemoryStream(Message.Record)).Play();
        }
    }
}
