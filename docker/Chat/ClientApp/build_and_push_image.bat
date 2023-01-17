cd ../../
docker build -t test-client -f Chat/ClientApp/Dockerfile .
docker login
docker tag test-client elza1428/test-client
docker push elza1428/test-client
pause