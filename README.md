# EasyCombinePictures

A software which easy to combine your pictures.

# 命令行接口 CLI

`EasyCombinePictures <picture_config_path> <work_id>`

`picture_config_path`：图片配置（工程）文件。

`work_id`：工作号。是该进程的不重复。标识符

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

<!-- 暂不支持
```
pic_arrangement: 'r3' #表示排成三竖列，注意数字必须是十进制数字，只支持数字。
```
-->

### 图片对象集

键名为`pics`。

值的格式：
```
pics:
 -
  path: 'dir/picture1.ext'
  text: 'A text.' #可以不写或设置成为text。
 -
  path: 'dir/picture2.ext'
```
