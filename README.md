# SharpSniper

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Often a Red Team engagement is more than just acheiving Domain Admin. Some clients will want to see if specific users in the domain can be compromised, for example the CEO.

SharpSniper is a simple tool to find the IP address of these users so that you can target their box.

It requires that you have privileges to read logs on Domain Controllers.

First it queries and makes a list of Domain contollers, then search for Log-on events on any of the DCs for the user you are looking for and then reads the most recent DHCP allocated logon IP address.

## Usage 

cmd.exe
```
C:\> SharpSniper.exe emusk DomainAdminUser DAPass123

User: emusk - IP Address: 192.168.37.130
```
Cobalt Strike
```
> execute-assembly /path/to/SharpSniper.exe emusk DomainAdminUser DAPass123

User: emusk - IP Address: 192.168.37.130
```
