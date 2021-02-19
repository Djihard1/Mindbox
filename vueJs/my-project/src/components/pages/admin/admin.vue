<template>
  <div class="admin">
    <div id="header">
      <h1 class="fleft"> Admin Panel </h1>
      <a class="fright" href="#" @click="logoutNow()">
        Logout
      </a>
    </div>
    <div id="sidebar">
      <ul class="nav">
        <li>
          <router-link to="/admin/users">
            Users
          </router-link>
        </li>
        <li>
          <router-link to="/admin/product">
            Product
          </router-link>
        </li>
        <li>
          <router-link to="/admin/orderitems">
            Order
          </router-link>
        </li>
        <li>
          <router-link to="/shop">
            Shop
          </router-link>
        </li>
      </ul>

    </div>
    <div id="main">
      <router-view/>
    </div>
  </div>
</template>

<script>
    export default {
        name: 'Admin',
        data () {
            return {
            }
        },
        mounted(){
            var token = localStorage.getItem("token");
            if (!token ){
                this.$router.push({name:"login"})
            }
        },
        methods:{
            logoutNow(){
                localStorage.setItem("token", "");
                this.$router.push({name:"login"})
            }

    }}
</script>



