# captcha-aspnet-mvc
Example: create captcha and read text from captcha image

### Issues:
+ Couln't read the text when it is deployed on IIS with Application Pool as specific users(service account).
  Fix:
  ```
  - Go to IIS > Application Pools
  - Select the application pool of app. Click Advantage Settings
  - Set `Load User Profile = True`. It needs to permission that SpeechSynthesizer is able to speak. 
  ```
