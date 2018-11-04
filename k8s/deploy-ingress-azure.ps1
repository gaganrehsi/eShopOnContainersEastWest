#These two lines added to resolve Azure AKS now requiring RBAC on NGINX by default
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/mandatory.yaml
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/provider/cloud-generic.yaml


kubectl patch deployment -n ingress-nginx nginx-ingress-controller --type=json --patch="$(cat nginx-ingress\publish-service-patch.yaml)"
kubectl apply -f nginx-ingress\azure\service.yaml
kubectl apply -f nginx-ingress\patch-service-without-rbac.yaml