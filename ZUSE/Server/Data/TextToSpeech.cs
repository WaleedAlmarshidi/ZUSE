
//using Google.Apis.Auth.OAuth2;
//using Google.Cloud.TextToSpeech.V1;

//namespace ZUSE.Server.Data
//{
//    public class TextToSpeech
//    {
//        private static GoogleCredential credentials { get; set; }
//        private static TextToSpeechClientBuilder tts_build { get; set; }
//        private static VoiceSelectionParams voiceSelection { get; set; }
//        private static AudioConfig audioConfig { get; set; }
//        private static TextToSpeechClient client { get; set; }

//        private static void Initialize()
//        {

//            credentials = GoogleCredential.FromFile(@"./GoogleTtsCreds.json");
//            //string access_token = "ya29.A0AVA9y1smSwRFtrhJeCUDKzQqLkcXqfwPGuMgc0H2pFiMaIKrs_wx4oHhjhC0DcFFJaOPqTXYQkcY3X4vNQqdrVRqfCIOav-mUJvP6ZA3PRTJGF72VUPOnwnbfxAcBO2P7QJ9W-3ZF-2S0y7jRtPFUmfz-Dk7MjNt5_fiyFjAxeXYRT1bmkUeCmHoAXq2DZddaGn3qOvgRsM1gZshS9dBseOHpIDYIncbsiWve_gwlTH-s295-cf9bC163iKaZq6tRfiw5XYYUNnWUtBVEFTQVRBU0ZRRTY1ZHI4Z2ZwbTFuVmFJMms0V3FFT1lYTmdPUQ0270";
//            //var credentials = GoogleCredential.FromAccessToken(accessToken: access_token);

//            tts_build = new TextToSpeechClientBuilder();
//            tts_build.Credential = credentials;
//            client = tts_build.Build();

            

//            // Build the voice request.
//            voiceSelection = new VoiceSelectionParams
//            {
//                LanguageCode = "ar-XA",
//                SsmlGender = SsmlVoiceGender.Female,
//                Name = "ar-XA-Wavenet-D"
//            };

//            // Specify the type of audio file.
//            audioConfig = new AudioConfig
//            {
//                AudioEncoding = AudioEncoding.Mp3
//            };

//            ////// Perform the text-to-speech request.
//            //Console.WriteLine(System.Text.Encoding.UTF8.GetString(response.AudioContent.ToByteArray()));
//            //string sss = "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\n";
//            // Write the response to the output file.
//            //using (var output = File.Create("Fuck.mp3"))
//            //{
//            //    Google.Protobuf.ByteString.FromBase64(sss).WriteTo(output);
//            //}
//        }
//        public static string GetBase64SpeechRepresentaion(string text)
//        {
//            var input = new SynthesisInput
//            {
//                Text = text
//            };
//            if (voiceSelection is null)
//                Initialize();
//            var response = client.SynthesizeSpeech(input, voiceSelection, audioConfig);
//            return response.AudioContent.ToBase64();
//        }


//    }
//}

