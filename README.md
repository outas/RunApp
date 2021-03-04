# RunApp

自定url协议打开本地windows应用

## 食用方式

1. 下载release内的压缩包
2. 将文件解压至C盘（解压完成后exe文件路径为C:\RunApp\RunApp.exe，需要放在其他位置的请自行修改reg文件）
3. 运行RegistryImport.reg
4. 配置RegisteredApps.xml，加入需要启动的应用
5. 浏览器输入 runapp://[key] 即可

## 传参方式

- 由于url中空格会被转义，所以使用";"分隔传参，如runapp://chrome;baidu.com，间隔的分号在运行应用时会转换为空格
- 支持多参数，但是不支持参数内容中出现";"
