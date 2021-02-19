import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/pages/Login'
import Admin from '@/components/pages/admin/admin'
import Users from '@/components/pages/admin/Users'
import Product from '@/components/pages/admin/Product'
import OrderItems from '@/components/pages/admin/OrderItems'
import Shop from '@/components/pages/shop/shop'
import ShopProducts from '@/components/pages/shop/ShopProducts'
import ShopProduct from '@/components/pages/shop/ShopProduct'



Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      redirect:{path:"login"}
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/admin',
      name: 'admin',
      component: Admin,
      redirect:{path:"/admin/users"},
      children:[
        {
          path: 'users',
          name: 'admin.users',
          component: Users
        },
        {
          path: 'product',
          name: 'admin.product',
          component: Product
        },
        {
          path: 'orderitems',
          name: 'admin.orderitems',
          component: OrderItems
        },



      ]
    },

    {
      path: '/shop',
      name: 'shop',
      component: Shop,
      redirect:{path:"/shop/products"},
      children:[
        {
          path: 'products',
          name: 'shop.products',
          component: ShopProducts
        },
        {
          path: 'product/:pid',
          name: 'shop.product',
          component: ShopProduct
        },
      ]
    }




  ],
  mode:"history"
})
