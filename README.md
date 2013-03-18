#git-wres

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