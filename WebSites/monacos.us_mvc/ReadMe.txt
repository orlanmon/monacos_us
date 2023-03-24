How To Gulp

http://www.davepaquette.com/archive/2014/10/08/how-to-use-gulp-in-visual-studio.aspx

Install Node.js

https://nodejs.org/en/

Node Package Manager

npm install gulp -g 



Debug Print of Files

$ npm install --save-dev gulp-debug


Error Handling in GULP

().on('error', function(e){
            console.log(e);
         }))


		 VS NodeJS Settings: Tools > Options > Projects and Solutions > External Web Tools and add the current nodejs path "C:\Program Files\nodejs".



Angular Minification Gotcha!


http://stackoverflow.com/questions/19671962/uncaught-error-injectorunpr-with-angular-after-deployment



/**********************************/
  monacos.us.model
/**********************************/

	Create Library Project

	Add ADoO.NET Entity Data Model 

	Builds EDMX

	 public Entities(string ConnectionString )
            : base(ConnectionString)
        {
           
        }
	
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Content>().HasKey(t => t.Content_ID);
            modelBuilder.Entity<ContentArea>().HasKey(t => t.ContentArea_ID);
            modelBuilder.Entity<Navigation_Items>().HasKey(t => t.Navigation_Item_ID);
            modelBuilder.Entity<Navigation_Types>().HasKey(t => t.Navigation_Type_ID);
            modelBuilder.Entity<NavigationItem_ResourceRole_Xref>().HasKey(t => t.Navigation_Role_Xref_ID);
            modelBuilder.Entity<Resource>().HasKey(t => t.Resource_ID);
            modelBuilder.Entity<Resource_Roles>().HasKey(t => t.Resource_Role_ID);
            modelBuilder.Entity<User>().HasKey(t => t.User_ID);
            modelBuilder.Entity<User_Role_Xref>().HasKey(t=> new { t.Resource_Role_ID, t.User_ID }  );
            modelBuilder.Entity<WebSession>().HasKey(t => t.WebSessionID);
            modelBuilder.Entity<WebSessionInformation>().HasKey(t => t.WebSessionInformationID);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


        }


		 class User_Role_Xref_Configuration : EntityTypeConfiguration<User_Role_Xref>
    {
        public User_Role_Xref_Configuration()
        {
            HasKey(t => t.Resource_Role_ID);
            HasKey(t => t.User_ID);
        }
    }




	Automapper

    PM>  Install-Package AutoMapper


	var config = new MapperConfiguration(cfg => {
    cfg.CreateMap<Source, Dest>();
});

IMapper mapper = config.CreateMapper();
var source = new Source();
var dest = mapper.Map<Source, Dest>(source);



DB Context Constructor Add In

 public Entities(string ConnectionString )
            :  base(ConnectionString)
        {

        }

		By convention EF uses either the field Id or [type name]Id as a Primary Key


		Had To Reinstall This For Razor Template Support

		Install-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform


/**********************************/
  Code Snippets:
/**********************************/

    
        $scope.log = function(message) {
          $log.debug(message);
        }
    


	Utlity Function Approach

	You Can Inject a function or a data model item into @scope


If you want a function to be available to all of your controllers, you might consider defining the method on $rootScope, instead of using a service:
myApp.run(function($rootScope) {
    $rootScope.daysInMonth = function(year, month) {
        return new Date(year, month+1,0).getDate();
    }
});

Then, due to prototypal scope inheritance, all of your controllers' scopes will have access to the method (without the need for dependency injection). You can call it in any controller like so:
function MyCtrl($scope) {
   console.log($scope.daysInMonth(12, 2012));
}​

JavaScript won't find function daysInMonth defined on $scope, so it will look in the parent scope (in this case the root scope) for the function, and it will find it.
 

 WCF
  This will return a JSON Object Representation; note not a  JSON String but JSON Object Model!!

  [WebGet(UriTemplate = "GetHeaderMenu?chka={CheckAuthentication}", ResponseFormat = WebMessageFormat.Json )]
    public List<MenuItem> GetHeaderMenu(bool CheckAuthentication)


	    <script src="~/scripts/lib/kendoui/kendoui-v2014.1.318/kendo.web.min.js"></script>
    <script src="~/scripts/lib/kendoui/kendoui-v2014.1.318/console.js"></script>
    <script src="~/scripts/lib/kendoui/kendoui-v2014.1.318/kendo.all.min.js"></script>


<link href="~/content/kendoui-v2014.1.318/examples-offline.css" rel="stylesheet" />
    <link href="~/content/kendoui-v2014.1.318/kendo.common.min.css" rel="stylesheet" />
    <link href="~/content/kendoui-v2014.1.318/kendo.rtl.min.css" rel="stylesheet" />
    <link href="~/content/kendoui-v2014.1.318/kendo.default.min.css" rel="stylesheet" />
    <link href="~/content/kendoui-v2014.1.318/kendo.black.min.css" rel="stylesheet" />




	 Kendo Menu


        $("#header_menu").kendoMenu({
            dataSource: [
                {
                    text: "Home", imageUrl: "/images//globe.png", url: "Default.aspx"
                },
                {
                    text: "Media", imageUrl: "/images//13.png",
                    items: [
                        { text: "Top News", imageUrl: "/images//07.png",  url: "News.aspx" },
                        { text: "Photo Galleries", imageUrl: "/images//29.png",  url: "Photos.aspx"  },
                        { text: "Videos ", imageUrl: "/images//13.png",  url: "http://www.youtube.com"  }
                        //{ text: "Youtube", imageUrl: "/images//13.png", content: "<a href='http://www.youtube.com' target='_blank'><img src='/images//globe.png'/></a>", encoded: true }

                    ]
                },
                {
                    text: "Listen Live", imageUrl: "/images//08.png", url: "ListenLive.aspx"
                },
                {
                    text: "ECR Rules", imageUrl: "/images//07.png", url: "Rules.aspx"
                },
                {
                    text: "Tech Net", imageUrl: "/images//24.png", url: "TechNet.aspx"
                },
                {
                    text: "About", imageUrl: "/images//15.png", url: "About.aspx"
                },
                {
                    text: "Admin", imageUrl: "/images//34.png",
                    items: [
                        { text: "Home Content", imageUrl: "/images//07.png", url: "HomeContent.aspx" },
                        { text: "News Content", imageUrl: "/images//07.png", url: "NewsContent.aspx" }
                    ]
                },
                {
                    text: "Contact", imageUrl: "/images//51.png", url: "Contact.aspx"
                }
            ], select: function (e) {

                // handle event
                // Now Check If URL Has WWW In It 
                // If So this is an external link!
                //alert(e.item.children[0].href);

                //e.item.childNodes[0].target = "_blank";
                //alert(e.item.childNodes[0].nameProp);

                if (e.item.childNodes[0].nameProp.indexOf("www") != -1 )
                {
                    e.item.childNodes[0].target = "_blank";
                }
                
            }
           });
           

		   // Add target attribute to open in new window 

			$(this).find(".k-link").attr("target", "_blank");




Routing:

Url.Action("someaction", "somecontroller", new { id = "123" }) generates: /somecontroller/someaction/123


 // Note Note Important!!!
 View Name is identical to action name so cshtml file name is identical to action method name.  You can override this with specifying another View Name in the Response 

 // Note Important!!!
 Note Parameters on Controller Action Method can be Populated From Model, Post Data ( Form or JSON Data with Post) , URI Route ( If configured), and query string!!



 // Some Script To Invoke a Action Method which returns HTML Content in String

 <script>

    OpenDialog = function(result)
    {

        //$("#ContactWorkflowDialog").html($.get('Url.Action("IndexDlgOneContent", "Contact")'));
        $("#ContactWorkflowDialog").html(result);


        $("#ContactWorkflowDialog").modal();
    }

    $(document).ready(function () {



        $("#btnOpenContactWorkflow").click(function () {


            $.ajax({

                url: 'http://localhost/monacos_us_mvc/Contact/IndexDlgOneContent',

                async: false,

                success: OpenDialog,

                error: function (xhr, textStatus, errorThrown) {

                alert("Error: " + textStatus);

            }

            });
 
        });
    });

</script>



Anchor Button with URL Action - Kool

 <a href='@Url.Action("Submit", "Home_Automation_Component", new { ControlDeviceID = @ControlDeviceInfoItem.ID, ControlDeviceCommand = "On", ControlDeviceValue = 100 })'>
                                <img src='@Url.Content("~/images/HomeAutomation/deviceonbutton.gif")' />
                            </a>


<form id="form_submit_control_device_command" action='http://localhost/monacos_us_mvc/Home_Automation_Component/Submit' method="post">

    <input type="hidden" id="ControlDeviceID" />
    <input type="hidden" id="ControlDeviceCommand" />
    <input type="hidden" id="ControlDeviceValue" />

</form>

	 function SubmitControlDeviceCommand(objSelect, ControlDeviceID) {

        debugger

        var objFormArray = $("#form_submit_control_device_command");

        objFormArray[0].ControlDeviceValue.value = $(objSelect).find('option:selected').val();

        objFormArray[0].ControlDeviceCommand.value = "Dim";
        objFormArray[0].ControlDeviceID.value = ControlDeviceID;

        objFormArray[0].submit();

        return true;

    }



// Git Reference

https://git-scm.com/
https://git-scm.com/docs





// Install git

https://git-scm.com/download/win


// Book

https://git-scm.com/book/en/v2/Getting-Started-About-Version-Control


// Bit Bucket

Create Repos# in bit Bucket


# repo #1

https://monacosofwaresolutions@bitbucket.org/teammonacosoftwaresolutions/monacos_us_mvc5.git

# repo #2

https://monacosofwaresolutions@bitbucket.org/teammonacosoftwaresolutions/azure_iot_prototpe.git




Create Team First 
Create Project
Create Repo for Project


// Administrator Account
UN:  monacos@monacos.us
PW:  Genesis19682Bits

// Previewer Account 

UN: orlando@monacos.us
PW: MSSPreviewer


//*************************************
// First Time Installing Repo

Example Repo #1 

#0 git init  - First Time ( installing git ) - Do this in root directory of every unique repo you want to create.

Note: Creates master branch and checks out automatically   

#1 Create Remote Repo in Bitbucket

#2 git remote add origin https://monacosofwaresolutions@bitbucket.org/teammonacosoftwaresolutions/monacos_us_mvc5.git

#3 git add --all   in Solution directory  ( Resursive )

#4 git commit -m "monacos.us ASP.NET MVC Web Application"   Note ( Master Brach and Master Checkout after this step!)

#5 git push -u origin master


Example Repo #2 

#0 Create Remote Repo in Bitbucket

#1 git init  - First Time ( installing git ) - Do this in root directory of every unique repo you want to create.

Note: Creates master branch and checks out automatically   


#2 git remote add origin https://monacosofwaresolutions@bitbucket.org/teammonacosoftwaresolutions/azure_iot_prototpe.git

#3 git add --all   in Solution directory  ( Resursive )

#4 git commit -m "monacos.us ASP.NET MVC Web Application"   Note ( Master Brach and Master Checkout after this step!)

#5 git push -u origin master   // Push Current Repo master branch to Origin Report master branch




//*************************************

//*************************************
//  Connect Existing Repository to to Bitbucket Repo

$ git remote add <remote name> <remote url>

#4 git remote add origin https://monacosofwaresolutions@bitbucket.org/teammonacosoftwaresolutions/monacos_us_mvc5_web.git






// origin is remote repository on BitBucket

// Push Current Repo to Origin Report master branch

#2  git push -u origin master


//*************************************

// Master is trunk!
// Note:  When a repo is created master branch created automatically.


//*************************************
1.Create the text file gitignore.txt.
2.Open it in a text editor and add your rules, then save and close.
3.Hold SHIFT, right click the folder you're in, then select Open command window here.
4.Then rename the file in the command line, with ren gitignore.txt .gitignore.
//*************************************




//*************************************
// Misc Git Odds and Ends 
//*************************************


git remote -v                // Shows you the Remote Repo

origin = remote repo =  https://orlanmon@bitbucket.org/monacosoftwaresolutions/monacos.us_repo.git    
 


git branch  // tell you what branch your in
git fetch -p   // Sync current branch
git branch <branch name>   // Creates Branch!!
git branch -d <branch name>   // Delete Current Branch
git remote -v                // Shows you the Remote Repo
git branch -a                // Show you all local and remote branches!!
git checkout <branch name>   // Switches to Branch
git checkout -b <branch name>	 // Switch Branches to new Branch  ( Creates New Branch! )
git branch -d                // Delete Local
// Note: For Above - shorthand for the following:  git branch <branch name>   git checkout <branch name>

git reflog show <branch>   // Shows Detailed Info On Branch ( Parent Branch )
git remote show origin     // Remote Origin Repo
git status -v              // Show Stage Revisions
git add file               // Add Modified, Deleted or New File to Staging to be ready for committing
git add * :/               // Add Modified or Any New Files to Stage ready for commit
git add --all              // recursive
git commit -m "Comment"   // Current Branch Commit

git pull  origin <branch name>   Pull and Merge! into current branch
git fetch  origin <branch name>  Pull and No Merge! into current branch
git commit --amend         // Reverst Commit
git reset HEAD <file name/path>    // Unstage File  ( Stage = Add/Update/Delete Flagged for Commit but not Committed)
git clean                   // Remove Unstaged



// Remove Files or Directories
git rm -r one-of-the-directories
git commit -m "Remove duplicated directory"
git push origin <your-git-branch> (typically 'master', but not always)


//*************************************

//*************************************
// Create Develop Branch Remotely - Modify and Then Commit

// This is create a Develop Branch off of Master Remotely

// New Repository Localy


git clone https://monacosofwaresolutions@bitbucket.org/teammonacosoftwaresolutions/monacos_us_mvc5.git

// Repository Existing
// Will Grab Currently Active Branch on Remote Repo
// Equivalent to git fetch   git merge

git pull https://monacosofwaresolutions@bitbucket.org/teammonacosoftwaresolutions/monacos_us_mvc5.git


git checkout master   - Make sure this is your selected branch

git checkout -b develop  - Create a New Branch off of master
OR
git checkout devlope  - Select Existing Branch

// Modify Files For Current Branch

git add new/modified files

git commit -m "New Changes 1.0"


// Push Current Branch ( develop ) to Origin Repo Develop Branch
git push origin develop

//*************************************


//*************************************
// Standard Revision Session with Remote Merge Via Pull Request After Push 
//*************************************
// Branch off of Development


// Get In Delopment Branch
git checkout develop 

// Create New Feature Branch off of Development
git checkout -b feature_one

// Make Modifications 
git add --all       // Add Modifications to Stage Ready for Commit
git status

// Commit Updates, Deletes, Adds  -
git commit -a -m "Comment"

git push origin feature_one


On Bit Bucket Create Pull Request from branch to develop for Peer Review 

Once Pull Request Peer Rview Completed Merge to develop

Note: Merge can occur localy or remotely - I prefer remotely with pull push and then pull request wih peer review


//*************************************


// Commit Updates, Deletes, Adds

git commit -a -m "Comment"      

// Push Current Branch to Origin Repo Develop Branch
git push origin develop



//******************************************
// Once Development is Ready For Production
//******************************************


git checkout master
git merge development


// Merge Develop to Master ( Work In Progress )

git checkout master

git pull origin master

git checkout develop

git pull origin develop

git merge master

git push origin master


// Suggested
(on branch development)$ git merge master
(resolve any merge conflicts if there are any)
git checkout master
git merge development (there won't be any conflicts now)





/***************************************************************
// Azure Connectivity and Authentication
/***************************************************************

Add Active Directory Authentication Library (ADAL)  to project



