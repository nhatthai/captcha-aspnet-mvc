# captcha-aspnet-mvc
Example: create captcha and read text from captcha image

### Issues:
+ Couln't read the text when it is deployed on IIS with Application Pool as specific users(service account).
   
  ```
  Fix: The app pool also needs permission to read HKU\.Default\Software\Microsoft\Speech
  - Go to IIS > Application Pools
  - Select the application pool of app. Click Advantage Settings
  - Set `Load User Profile = True`. 
  ```
