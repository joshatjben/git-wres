#git-wres

__Development had just stared.  This is still a work in progress!__

Custom git command to manage JavaScript web resources in Microsoft CRM 2011.

##Usage

```bash
cd [project-dir-here]
git wres init
git wres add -n dev-default -s default http://crmdev/HCCDev
git wres pull dev-default
```

make some changes to the files

```bash
git wres push dev-default
```

## commands

### add [options] url
#### options
* -n name of the wres remote connection
* -s name of the solution to point to
#### url
* the url of the crm including organization name