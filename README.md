# identityframework-demo
This repo is a simple demo of how to get started with ASP.NET Identity Framework. No database is needed for this demo to work, user
information is saved to `\bin\users.txt`.

Here is the slide: https://goo.gl/9nARUp

## Running the project
- Postman: https://www.getpostman.com/apps
- Import collection: https://www.getpostman.com/collections/b1fb0fa3bb0cabf392b2

## Branch description
- **master**
  - Complete branch which has all the features
- **1-project-setup**
  - Demonstrate how the project is setup with a test controller to make sure that the OWIN pipeline is working with DI (Autofac)
- **2-identity-framework**
  - Setup ASP.NET Identity Framework without any implementations
- **3-userstore**
  - Implement IUserStore
- **4-queryableuserstore**
  - Implement IQueryableUserStore
- **5-passwordstore**
  - Implement IUserPasswordStore
- **6-usersecuritystamp**
  - Implement IUserSecurityStampStore
- **7-useremailstore**
  - Implement IUserEmailStore
  - `Identity\Managers\UserManagers.cs`: how you can override the base method of `UserManager`
  - `AutofacModule.cs`: how to control email verification token's lifespan and inject `EmailService`
  - `Identity\Providers\EmailService.cs`: how the email service is implemented if you want to send SMS this is the exact same interface that you have to implement
- **8-userclaimstore**
  - Implement IUserClaimStore
  - `Identity\Models\UserClaim.cs`: this is just a class for mapping out the user claims
- **9-oauth**
  - Implement OAuth authentication
  - `Startup.cs`: how OAuth is added and setup to the pipeline
  - `Identity\Providers\AuthorizationServerProvider.cs`: how to resolve an instance from dependency resolver, authenticate user and add claims to the token
  - `Controllers\UserProfileController.cs`: how to extract information from the token without hitting the database
  
