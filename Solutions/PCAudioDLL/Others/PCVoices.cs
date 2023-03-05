namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class PCVoices
    {
        public ExWaveOut[] VoicesArray = new ExWaveOut[61];
        internal int lastRequestIndex = 9;

        //-------------------------------------------------------------------------------------------------------------------------------
        internal int RequestVoice(bool isLooped, DebugConsole outputConsole)
        {
            //Avoid Get out of bounds
            lastRequestIndex++;
            if (lastRequestIndex >= VoicesArray.Length)
            {
                lastRequestIndex = 10;
            }

            //Update Object
            VoicesArray[lastRequestIndex].Active = true;
            VoicesArray[lastRequestIndex].Played = true;
            VoicesArray[lastRequestIndex].Playing = true;
            VoicesArray[lastRequestIndex].Looping = isLooped;
            VoicesArray[lastRequestIndex].Reverb = true;
            VoicesArray[lastRequestIndex].Stop_ = false;
            VoicesArray[lastRequestIndex].Stopped = false;
            VoicesArray[lastRequestIndex].Locked = true;

            //Inform user
            outputConsole.WriteLine(string.Format("ES-> ES_RequestVoiceHandle() = {0}", lastRequestIndex));
            outputConsole.WriteLine("Voice::Play");

            return lastRequestIndex;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PreCloseVoice(int voiceIndex, DebugConsole outputConsole)
        {
            VoicesArray[voiceIndex].Active = true;
            VoicesArray[voiceIndex].Played = false;
            VoicesArray[voiceIndex].Playing = false;
            VoicesArray[voiceIndex].Looping = false;
            VoicesArray[voiceIndex].Reverb = false;
            VoicesArray[voiceIndex].Stop_ = true;
            VoicesArray[voiceIndex].Stopped = false;
            VoicesArray[voiceIndex].Locked = true;

            //Inform User
            outputConsole.WriteLine(string.Format("ES-> ES_AudioHasEnded() = {0} Ok.", voiceIndex));
            outputConsole.WriteLine(string.Format("ES-> ES_UnLockVoiceHandle() = {0}", voiceIndex));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void CloseVoice(int voiceIndex)
        {
            VoicesArray[voiceIndex].Active = false;
            VoicesArray[voiceIndex].Played = false;
            VoicesArray[voiceIndex].Playing = false;
            VoicesArray[voiceIndex].Looping = false;
            VoicesArray[voiceIndex].Reverb = false;
            VoicesArray[voiceIndex].Stop_ = false;
            VoicesArray[voiceIndex].Stopped = false;
            VoicesArray[voiceIndex].Locked = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void CloseAllVoices(DebugConsole outputConsole)
        {
            for (int i = 10; i < VoicesArray.Length; i++)
            {
                VoicesArray[i].Active = false;
                VoicesArray[i].Played = false;
                VoicesArray[i].Playing = false;
                VoicesArray[i].Looping = false;
                VoicesArray[i].Reverb = false;
                VoicesArray[i].Stop_ = false;
                VoicesArray[i].Stopped = false;
                VoicesArray[i].Locked = false;
            }

            //Add Debug Test
            outputConsole.WriteLine("ES-> ES_SFXRemove()");
            outputConsole.WriteLine(string.Format("ES-> psiSampleKeyOff( {0} )", lastRequestIndex));
            outputConsole.WriteLine("Voice::Stop");
            outputConsole.WriteLine(string.Format("ES-> ES_AudioHasEnded() = {0} Ok.", lastRequestIndex));
            outputConsole.WriteLine(string.Format("ES-> ES_UnLockVoiceHandle() = {0}", lastRequestIndex));
            outputConsole.WriteLine("ES-> Sfx->KeyOffWait OK.");
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
