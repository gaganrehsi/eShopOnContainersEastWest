kubectl apply -f ingress.yaml

# Deploy nginx-ingress core files
kubectl apply -f nginx-ingress\namespace.yaml
kubectl apply -f nginx-ingress\default-backend.yaml
kubectl apply -f nginx-ingress\configmap.yaml 
kubectl apply -f nginx-ingress\tcp-services-configmap.yaml
kubectl apply -f nginx-ingress\udp-services-configmap.yaml
kubectl apply -f nginx-ingress\without-rbac.yaml

#These two lines added to resolve Azure AKS now requiring RBAC on NGINX by default
#kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/mandatory.yaml
#kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/provider/cloud-generic.yaml




