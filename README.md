Ionic 4 and IdentityServer Starter
---------------

<b>IdentityServer4, Ionic 4, Oidc</b>
<p>Pre-Generated with <a href="https://github.com/DooMachine/MicroBoiler">MicroBoiler</a></p>

Getting started
---------------

```
# Clone the repository
git clone git@github.com:DooMachine/Ionic_IdentityServer4.git
cd Ionic_IdentityServer4

# Set your ASPNETCORE_ENVIRONMENT environment to Development
# Remove this git config
rm -rf .git 
# Start Your PostgreSql
Set your postgresql username and password in MicroStarter.Identity/src/Host/Startup.cs
# run migrations 
bash migrations.dev.sh (linux - mac)
bash update.dev.sh (linux - mac)
# start identity server with seed option to populate configuration
cd MicroStarter.Identity/src/Host
dotnet run /seed
# start ionic with cordova () 
cd ionic_4_cli
ionic cordova run browser -l
```