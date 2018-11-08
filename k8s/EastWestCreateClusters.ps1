az group create -n eshopdev -l eastus2

az aks create -g eshopdev -n eshopaksdev --node-count 2 --disable-rbac --generate-ssh-keys

az aks get-credentials  -g eshopdev -n eshopaksdev

kubectl config use-context eshopaksdev

./deploy-ingress.ps1

./deploy-ingress-azure.ps1

az group create -n eshopeast2 -l eastus2

az aks create -g eshopeast2 -n eshopakseast --node-count 2 --disable-rbac --generate-ssh-keys

az aks get-credentials  -g eshopeast2 -n eshopakseast

kubectl config use-context eshopakseast

./deploy-ingress.ps1

./deploy-ingress-azure.ps1

az group create -n eshopwest2 -l westus2

az aks create -g eshopwest2 -n eshopakswest --node-count 2 --disable-rbac --generate-ssh-keys

az aks get-credentials  -g eshopwest2 -n eshopakswest

kubectl config use-context eshopakswest

./deploy-ingress.ps1

./deploy-ingress-azure.ps1

kubectl config use-context eshopaksdev

./deploy.ps1 -buildImages $false -configFile conf_local.yaml -imageTag latest -pushImages $false -registry fseshop.azurecr.io -dockerUser FSeshop -dockerPassword V0C7hGc9pfXQmysQjtAr/BdPXHSSvLeg -useACR $true

kubectl config use-context eshopakseast

./deploy.ps1 -buildImages $false -configFile conf_local.yaml -imageTag latest -pushImages $false -registry fseshop.azurecr.io -dockerUser FSeshop -dockerPassword V0C7hGc9pfXQmysQjtAr/BdPXHSSvLeg -useACR $true

kubectl config use-context eshopakswest

./deploy.ps1 -buildImages $false -configFile conf_local.yaml -imageTag latest -pushImages $false -registry fseshop.azurecr.io -dockerUser FSeshop -dockerPassword V0C7hGc9pfXQmysQjtAr/BdPXHSSvLeg -useACR $true
