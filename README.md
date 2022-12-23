# Chat

## How to run locally

To run this project you may need following:
- Visual Studio 2022 & .NET 6 (Easy to install with help of Visual Studio Installer)
- MS Sql Server (LocalDB) - usually goes with Visual Studio, so it can be already installed, if not - use Visual Studio Installer
- Some of package managers: `npm` / `yarn` - whatever you like. I used `npm`
- Angular CLI tools: can be installed `npm install -g @angular/cli` (if you use `npm`)

1. Run backend part using Kestrel web-server

![image](https://user-images.githubusercontent.com/75540967/209351017-d5223688-98d7-4f35-b02a-f6f90d194aa6.png)
![image](https://user-images.githubusercontent.com/75540967/209351223-06570b29-abfe-47b2-9ab5-04d0b2c39d04.png)

2. Your browser will be launched

You will see the following url (server is listening requests on localhost on this port - 5089)

![image](https://user-images.githubusercontent.com/75540967/209351492-6f4cc919-0902-4ba8-802f-a3bef08e8854.png)

3. This is ok you got this error at the screenshot below

![image](https://user-images.githubusercontent.com/75540967/209351873-429e014c-ae34-447c-8e3c-24127bb77d7a.png)

4. If you don't need frontend part - that is all - feel free to make api calls. 

5. To run frontend part go to ClientApp directory

![image](https://user-images.githubusercontent.com/75540967/209352331-956e59e0-6929-4374-a38d-aac46a670b0a.png)

6. Open cmd and run `npm install` command to install `node_modules`

! This step is only needed when you either run frontend part for the first time or want to update modules according to `package.json`

![image](https://user-images.githubusercontent.com/75540967/209352414-2e09b02e-30ee-48b2-b030-f999d5ed3c0a.png)

7. Ignore all warnings:) If ran this command for the 1st time `node_modules` directory will appear

![image](https://user-images.githubusercontent.com/75540967/209353244-1fdaaa4d-3740-4e83-9c55-8af062126b50.png)

8. Now you can run frontend part with `ng serve` command

![image](https://user-images.githubusercontent.com/75540967/209353460-20424c86-c2f1-4388-9577-f3099a9fb3c1.png)

9. In case of success you'll get something like on the screenshot provided below

![image](https://user-images.githubusercontent.com/75540967/209353627-34912603-e8e5-41b5-9619-93f48f7c46a7.png)

10. By default angular live development server is running on 4200 port, so you can return to your backend error in browser

![image](https://user-images.githubusercontent.com/75540967/209353965-4fb3819d-42e9-45aa-86dd-4137f3328689.png)

11. Almost done! Just refresh your page

![image](https://user-images.githubusercontent.com/75540967/209354066-7e40f26e-9821-464a-852b-90aa83c004fd.png)

12. This is what you get (for now)

![image](https://user-images.githubusercontent.com/75540967/209354378-5ef81d8b-073c-4905-846a-9b6a92932940.png)


