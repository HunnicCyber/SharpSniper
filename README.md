# SharpSniper


Often a Red Team engagement is more than just achieving Domain Admin. Some clients will want to see if specific users in the domain can be compromised, for example the CEO.

SharpSniper is a simple tool to find the IP address of these users so that you can target their box.

It requires that you have privileges to read logs on Domain Controllers.

First it queries and makes a list of Domain contollers, then search for Log-on events on any of the DCs for the user you are looking for and then reads the most recent DHCP allocated logon IP address.

N.B. Build can also target .net framework v3.5 if needed.

## Usage 

cmd.exe (Supply credentials)
```
C:\> SharpSniper.exe emusk DomainAdminUser DAPass123

User: emusk - IP Address: 192.168.37.130
```
cmd.exe (Current authentication token e.g. Mimikatz pth)
```
C:\> SharpSniper.exe emusk

User: emusk - IP Address: 192.168.37.130
```
Cobalt Strike (Supply credentials)
```
> execute-assembly /path/to/SharpSniper.exe emusk DomainAdminUser DAPass123

User: emusk - IP Address: 192.168.37.130
```
Cobalt Strike (Beacon's token)
```
> execute-assembly /path/to/SharpSniper.exe emusk

User: emusk - IP Address: 192.168.37.130
```

## Author

Tom Kallo
