<%@ Page Language="C#" AutoEventWireup="true" Inherits="BabyPlan.TaoBao.login_demo" Codebehind="login-demo.aspx.cs" %>
<html>
<head>
<title>淘宝登录按钮Demo(.net)</title>
<script src="http://a.tbcdn.cn/apps/top/x/sdk.js?t=120418.js"></script>
<script>
//配置系统参数
    TOP.init({
    appKey: "<%=config.AppKey%>", /*appkey */
    channelUrl: "<%=config.ChannelUrl%>"/* channel页面的URL */
});
</script>
</head>
<body>
<h1>淘宝组件SDK DEMO(.net)</h1>
<div id="taobao-login"></div>
<script language="javaScript">
    TOP.ui('login-btn', {
        container: '#taobao-login', //组件容器,通过id指定容器
        showAvatar: true, //是否显示头像
        showUserLink: true, //是否生成链接 
        showLogout: true, //是否显示退出
        type: "4,4",
        callback: {
            login: null, //登录成功回调函数
            logout: null//退出成功回调函数
        }
    })    
</script>

<div class="S_Widget"></div> 
<script>
    //配置组件参数
    TOP.ui('sku', {
        container: '.S_Widget', //组件容器
        text: '加入购物车',
        itemId: '14842915804' //加入购物车的商品编号        
    });
</script>

<div class="S_Widget2"></div> 
<script>
    //配置组件参数
    TOP.ui('sku', {
        container: '.S_Widget2', //组件容器
        text: '加入购物车',
        itemId: '14842915804' //加入购物车的商品编号        
    });
</script>

<div class="mini-cart"></div>
<script>
//配置组件参数
TOP.ui('minicart',{
    container: '.mini-cart',  //组件容器
    position: 'right',//默认为top; top为横向购物车，right为纵向购物车
    width: '50',
    height: '100'
});

</script>
</body>
</html>