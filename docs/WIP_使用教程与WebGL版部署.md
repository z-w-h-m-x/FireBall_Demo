# 使用教程与WebGL版部署

## 须知

使用Unity 2021.3 版本 (f1)

## [WIP]使用【未完成】

请注意这个是代码的使用例，有备注以外的所有代码都需要有调用unity的API

|函数/方法/模块名|引用其他模块|备注|
|-|:-:|-|
[存档模块](#存档模块)|-


### 存档模块

由```ArchiveData``` ```ArchiveDo``` ```ArchiveList``` ```ArchiveManager```四个C#脚本组成，```ArchiveList```储存存档的数据格式，在此更改。 ```ArchiveManager```需要挂载到游戏对象上（任意一个，一次就行）才可以使用/初始化模块。

## 安装或部署

### Android

安装apk包，直接运行，这个版本可以使用存档功能

### webgl版

推荐使用nginx服务器

直接将编译后的文件放进网站根目录即可

#### 从零开始的在windows平台上使用nginx服务器软件部署（零经验）

这个部分十分的~~简洁~~简易，和零基础

首先到nginx的官网[下载页面](https://nginx.org/en/download.html)下载Windows版本的nginx

推荐使用稳定版本（stable version）。随后将压缩解压到任意目录下（但是建议全英文/数字目录）

解压后的目录实例
```
nginx-x.xx.x
├─conf
├─contrib
│  ├─unicode2nginx
│  └─vim
│      ├─ftdetect
│      ├─ftplugin
│      ├─indent
│      └─syntax
├─docs
├─html
├─logs
└─temp
    ├─client_body_temp
    ├─fastcgi_temp
    ├─proxy_temp
    ├─scgi_temp
    └─uwsgi_temp
```
html为默认的网站根目录（具体看配置文件（conf/nginx.conf）

将编译好的webgl版压缩包全部解压到html文件夹下（网站根目录）
这一步html文件夹下文件示例
```
html
│  index.html
│
├─Build
│      fireball_0_0_0_1_005W.data
│      fireball_0_0_0_1_005W.framework.js
│      fireball_0_0_0_1_005W.loader.js
│      fireball_0_0_0_1_005W.wasm
│
├─StreamingAssets
│      UnityServicesProjectConfiguration.json
│
└─TemplateData
        favicon.ico
        fullscreen-button.png
        progress-bar-empty-dark.png
        progress-bar-empty-light.png
        progress-bar-full-dark.png
        progress-bar-full-light.png
        style.css
        unity-logo-dark.png
        unity-logo-light.png
        webgl-logo.png
```

随后运行nginx即可，访问127.0.0.1即可（本地回环地址，访问本机用

关于运行nginx，推荐在cmd等shell中运行取代使用双击直接运行