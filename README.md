﻿# Clone User Setup
XrmToolBox.CloneUserSetup 

**Clone User Setup** will clone Business Unit, Security Roles, and Team memberships from a Source User to a Target User.  This is perfect for setting up a new user based on an existing user.  It is also useful when testing.  In particular, when reproducing an issue as a particular user -- as you can clone the user with issues to a test user that you have access to log in as.

> **⚠ IMPORTANT NOTE**  
> The **Target User**'s Business Unit, Security Roles, and Team Memberships will be cleared and replaced with those from the **Source User**.
 
## Installation
Install the plugin from the XrmToolBox Store or you can unzip the [prebuilt assembly](https://github.com/donschia/XrmToolBox.CloneUserSetup/tree/master/Schiavone.XrmToolBox.CloneUserSetup/Deployment) to your Plugins folder.  Launch the `Clone User Setup` tool.
![Store Tool](docs/img/CloneUserSetupTool.png)
## Usage
1. Pick a **Source User** and a **Target User** and click the `COPY Business Unit, Security Roles, and Teams` button.
![Alt text](docs/img/CloneUserSetup1.png)
- Click `Yes` to Confirm.
![Alt text](docs/img/CloneUserSetup2.png)

2. The system will set the Business Unit on the **Target User** to match the **Source User**, then clone the Security Roles and Team Memberships.  
    ![Alt text](docs/img/CloneUserSetup3.png)

3. Finished!  Clicking `OK` will refresh the **Target User**.  
![Alt text](docs/img/CloneUserSetup4.png)
-  The Source User's Busines Unit, Roles, and teams have been cloned to the Target User.
![Alt text](docs/img/CloneUserSetup5.png)

## Notes:
- Click the `Open` button on the **Source User** or the **Target User** to navigate to the user record in Dynamics 365.  It defaults to the legacy interface where you can further administer the Security Roles with ease.
If you prefer the newer UCI interface simply check the `Open Links in UCI Interface` box and then click the `Open` button.
 
- To debug this plugin you have to [update the link to the included XrmToolbox executable](
https://www.xrmtoolbox.com/documentation/for-developers/debug/) in your build folder. 
![Debugging in Visual Studio](docs/img/Debugging_VisualStudio.png)

