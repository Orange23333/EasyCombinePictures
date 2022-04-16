# EasyCombinePictures

A software which easy to combine your pictures.

# 命令行接口 CLI

`ezcmbpic <arguments>`

## 参数

### config

打开配置文件

`--picture-config <path>`或`--pc <path>`

`path`：图片配置文件路径。

## 配置文件

采用`YAML`格式进行编写。

```
pic_config:
 <配置选项>
pics:
 <图片对象>
```

### 配置选项

键名为`pic_config`，后面的值是配置选项的内容。没有可以填写`~`。

#### 排列格式

键名为`pic_arrangement`。

```
pic_arrangement: 'l' #表示排成一横行。
```

```
pic_arrangement: 'r' #表示排成一竖列。
```

```
pic_arrangement: 'r3' #表示
```

### 图片对象