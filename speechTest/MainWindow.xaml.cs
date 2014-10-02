using System;
using System.Speech.Recognition;
using System.Windows;

namespace speechTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // We need one a deez
        SpeechRecognitionEngine Recognizer = null;


        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;


            // Set up the Speech Recognition Engine
            Recognizer = new SpeechRecognitionEngine();
            // Make sure the Kinect is the default audio input device (Control Panel - Sound)
            Recognizer.SetInputToDefaultAudioDevice();
            // Load a dictation grammar
            Recognizer.LoadGrammar(new DictationGrammar());
            // Handle the recognized event
            Recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }




        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Start recognizing
            Recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }


        void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Show the confidence and the speech recognized
            SpeechTextBlock.Text +=
                "(" + e.Result.Confidence.ToString("##.##") + ") "
                + e.Result.Text + "\r\n";

        }


        void MainWindow_Closed(object sender, EventArgs e)
        {
            // Kill em all!
            Recognizer.RecognizeAsyncStop();
            Recognizer.Dispose();
        }
    }
}
