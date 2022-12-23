// not used. look at src/proxy.conf.json
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:20353';

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/library/get-libraries",
      "/book/get-books",
      "/book/get-book-pdf",
      "api/chat/get-chats/*"
   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
