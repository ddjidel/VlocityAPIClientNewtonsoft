# Call Vlocity APIs from C#
## Setup
### Create a Connected App in your Salesforce Org
#### 1 - Setup => Create => Apps => Connected Apps => New
#### 2 - Enter a "Connected App Name" and a "Contact Email"
#### 3 - Select "Enable OAuth Settings"
#### 4 - "Callback URL": http://localhost
#### 5 - "Selected OAuth Scopes": Select them all and "Add"
#### 6 - Save. You should a message saying "Allow from 2-10 minutes for your changes to take effect on the server before using the connected app"
#### 7 - Click "Continue"
#### 8 - Copy the "Consumer Key" and "Consumer Secret" (click on "Click to reveal" for that one)
### Update the C# App
