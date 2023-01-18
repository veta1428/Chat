cd ../
docker build -t test-chat -f Chat/Dockerfile .
docker login
docker tag test-chat elza1428/test-chat
docker push elza1428/test-chat
pause