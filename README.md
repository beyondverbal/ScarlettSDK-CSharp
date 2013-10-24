ScarlettSDK-CSharp
==================

Usage:

  var EmotionsSessionParameters = SessionParameters.Create().WithDataFormat(new WAVFormat());

  var session = new EmotionsAnalyzer().InitializeSession(EmotionsSessionParameters);
            
  session.NewAnalysis += session_NewAnalysis;
  session.ProcessingDone += session_ProcessingDone;
  
  session.AnalyzeAsync(voiceStream);
  
  
