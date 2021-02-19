<template>
  <div class="login">
    <div class="loginHeader">
      User Login
    </div>
    <div class="loginContainer">
      <table>
      <tr>
        <td>UserName</td>
        <td>:</td>
        <td><input type="text" placeholder="Username" v-model="user.username"></td>
      </tr>
        <tr>
          <td>Password</td>
          <td>:</td>
          <td><input type="password" placeholder="Password" v-model="user.password"></td>
        </tr>
        <tr>
          <td></td>
          <td>:</td>
          <td><button class="addBtn" @click="loginNow()"> Login </button></td>
        </tr>
      </table>
    </div>
    <div>///////////////////////////////////////////////////////////////////////////////////// </div>
    <div class="loginHeader">
      Registration
    </div>
    <div class="loginContainer">
      <table>
        <tr>
          <td>UserName</td>
          <td>:</td>
          <td><input type="text" placeholder="Username" v-model="reg.username"></td>
        </tr>
        <tr>
          <td>Password</td>
          <td>:</td>
          <td><input type="password" placeholder="Password" v-model="reg.password"></td>
        </tr>
        <tr>
          <td>Role</td>
          <td>:</td>
          <td>
            <select type="text" placeholder="Role" v-model="reg.Role">
              <option>Manager</option>
              <option>Сustomer</option>
              </select>
          </td>
        </tr>
        <tr>
          <td>Name</td>
          <td>:</td>
          <td><input type="text" placeholder="Name" v-model="reg.Name"></td>
        </tr>
        <tr>
          <td>Code</td>
          <td>:</td>
          <td><input type="date" placeholder="Code" v-model="reg.Code"></td>
        </tr>
        <tr>
          <td></td>
          <td>:</td>
          <td><button class="addBtn" @click="registerNewUser()"> Login </button></td>
        </tr>
      </table>
    </div>
  </div>
</template>
<script>
    export default {
        name: 'Login',
        data () {
            return {
                user:{
                  username:"",
                  password:""
                },
                reg:{
                    username:"",
                    password:"",
                    Role:"",
                    Name:"",
                    Code:"",
                }
            }
            },
        methods:{
            loginNow(){
               // console.log(this.user);
              this.$eventBus.$emit("loadingStatus", true);
              this.$axios.post("http://localhost:56750/Auth/login", this.user).then(res =>{
                  this.$eventBus.$emit("loadingStatus", false)
                  if(res.data.message){
                          this.$iziToast.error({
                          title: 'Error',
                          message: res.data.message,
                      });
                  }
                  else {

                      localStorage.setItem("token", res.data.data);
                      this.$axios.defaults.headers.common['Authorization'] = 'token' +localStorage.getItem("token");
                  }
                  })
                let url = "http://localhost:56750/Auth/" + this.user.username;
                this.$axios.get(url).then(res =>{
                    localStorage.setItem("customerid", res.data.data.id);
                    localStorage.setItem("role", res.data.data.role);
                    let role = localStorage.getItem("role");
                    if(role =="Manager"){
                        this.$router.push({name:"admin"})
                    }
                    else {
                        this.$router.push({name:"shop"})
                    }
                })
            },
            registerNewUser(){
                //console.log(this.reg);
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.post("http://localhost:56750/Auth/Register", this.reg).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.success){
                        localStorage.setItem("token", res.data.data);
                        this.$axios.defaults.headers.common['Authorization'] = 'token' +localStorage.getItem("token");
                        this.$router.push({name:"admin"})
                        //var xx = localStorage.getItem("token")
                        //console.log(res);
                    }
                    else {
                        this.$iziToast.error({
                            title: 'Error',
                            message: "\n" +
                                "The user already exists.",
                        });
                    }
                })
            }
        }
    }
</script>



