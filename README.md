ScarlettSDK-CSharp
==================

Usage:

  var EmotionsSessionParameters = SessionParameters.Create().WithDataFormat(new PCMFormat(8000, 16, 1));

  var session = new EmotionsAnalyzer().InitializeSession(EmotionsSessionParameters);
            
  session.NewAnalysis += session_NewAnalysis;
  session.ProcessingDone += session_ProcessingDone;
  
  session.AnalyzeAsync(voiceStream);
  
  
