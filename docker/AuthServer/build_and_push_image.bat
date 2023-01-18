cd ../
docker build -t test -f AuthServer/Dockerfile .
docker login
docker tag test elza1428/test
docker push elza1428/test
pause