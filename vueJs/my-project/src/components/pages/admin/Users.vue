<template>
  <div>
    <modal modalHeading=" Add new User" :cond = "showingAddModal" @modalClose = "showingAddModal = false" >
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
          <td><input type="date" placeholder="Code" v-model="reg.Code" id="Code"></td>
        </tr>
        <tr>
          <td>Address</td>
          <td>:</td>
          <td><input type="text" placeholder="Address" v-model="reg.Address"></td>
        </tr>
        <tr>
          <td>Discount</td>
          <td>:</td>
          <td> <select type = "number" placeholder="Discount" v-model.number="reg.Discount" >
            <option>0</option>
            <option value=10>10</option>
            <option>20</option>
            <option>30</option>
            <option>40</option>
            <option>50</option>
            <option>60</option>
            <option>70</option>
            <option>80</option>
            <option>90</option>
          </select></td>
        </tr>
        <tr>
          <td>
          </td>
          <td>
            <button class="btnSave" @click= "addNewUser()"> Save</button>
          </td>
        </tr>
      </table>
    </modal>
    <modal modalHeading=" Update this user" :cond = "showingEditModal" @modalClose = "showingEditModal = false" >
      <table>
        <tr>
          <td>Login</td>
          <td>:</td>
          <td><input type="text" placeholder="Username" v-model="clickedUser.login"></td>
        </tr>
        <tr>
          <td>Role</td>
          <td>:</td>
          <td>
            <select type="text"  placeholder="Role" v-model="clickedUser.role">
              <option>Manager</option>
              <option>Сustomer</option>
            </select>
          </td>
        </tr>
        <tr>
          <td>Name</td>
          <td>:</td>
          <td><input type="text" placeholder="Name" v-model="clickedUser.name"></td>
        </tr>
        <tr>
          <td>Code</td>
          <td>:</td>
          <td><input type="date" placeholder="Code" id="newCode" v-model="clickedUser.code"></td>
        </tr>
        <tr>
          <td>Address</td>
          <td>:</td>
          <td><input type="text" placeholder="Address" v-model="clickedUser.address"></td>
        </tr>
        <tr>
          <td>Discount</td>
          <td>:</td>
          <td> <select type = "number" placeholder="Discount" v-model.number="clickedUser.discount">
            <option>0</option>
            <option>10</option>
            <option>20</option>
            <option>30</option>
            <option>40</option>
            <option>50</option>
            <option>60</option>
            <option>70</option>
            <option>80</option>
            <option>90</option>
          </select></td>
        </tr>
        <tr>
          <td>
          </td>
          <td>
            <button class="btnSave" @click= "updateUser()"> Update</button>
          </td>
        </tr>
      </table>
    </modal>

    <modal modalHeading=" Delete user" :cond = "showingDeleteModal" @modalClose = "showingDeleteModal = false" >
      <h2>You are going to delete the user '{{clickedUser.name}}'</h2>
      <table>
        <tr>
        </tr>
        <tr>
          <td>
          </td>
          <td>
            <button class="btnSave" @click= "DeleteUser()"> Yes</button>
          </td>
          <td>
            <button class="btnSave" @click= "showingDeleteModal = false"> No</button>
          </td>
        </tr>
      </table>
    </modal>

    <h2 class="fleft">Users</h2>
    <button class="addBtn fright" @click="showingAddModal = true">Add New</button>
    <div class="clear"></div>
    <hr>
    <table class="nice-table">
      <tr>
        <th>ID</th>
        <th>Login</th>
        <th>Name</th>
        <th>Role</th>
        <th>Code</th>
        <th>Address</th>
        <th>Discount</th>
        <th>Edit</th>
        <th>delete</th>
      </tr>

      <tr v-for="user in Users">
        <td>{{user.id}}</td>
        <td>{{user.login}}</td>
        <td>{{user.name}}</td>
        <td>{{user.role}}</td>
        <td>{{user.code}}</td>
        <td>{{user.address}}</td>
        <td>{{user.discount}}</td>
        <td><button class="edit" @click=" showingEditModal = true , clickedUser = user" >Edit</button></td>
        <td><button class="delete" @click="showingDeleteModal = true, clickedUser = user">Delete</button></td>
      </tr>
    </table>
  </div>
</template>

<script>
    export default {
        name: 'Users',
        data () {
            return {
                showingAddModal:false,
                showingEditModal:false,
                showingDeleteModal:false,
                reg:{
                    username:"",
                    password:"",
                    Role:"",
                    Name:"",
                    Code:"",
                    Address:"",
                    Discount:0
                },
                Users:[],
                clickedUser:{},
            }
        },
        mounted(){
           // console.log("Mounted")
            this.init();
        },
        methods:{
            init(){
                let userToken= 'Bearer' +" " +localStorage.getItem("token");

                console.log(userToken)
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.get("http://localhost:56750/Api/Customer/GetAll", {headers:{ 'Authorization': userToken}} ).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.error){
                        this.$iziToast.error({
                            title: 'Error',
                            message: res.data.message,
                        });
                    }
                    else {
                       this.Users = res.data.data;
                        //console.log(this.Users)
                    }
                })

            },
            addNewUser(){
                //console.log(this.reg.Discount);
                //let a = this.reg.Discount.number()
//todo!!!!
                if (!this.reg.Name ){{
                    this.$iziToast.error({
                        title:'error',
                        message:'Name can not be empty'
                    })
                    var nameInput = document.getElementById("newUserName")
                    return;
                }}
                if (!this.reg.Code ){{
                    this.$iziToast.error({
                        title:'error',
                        message:'Code can not be empty'
                    })
                    var codeInput = document.getElementById("Code")
                    return;
                }}
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.post("http://localhost:56750/Auth/Register", this.reg).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.success){
                     //   localStorage.setItem("token", res.data.data);
                        this.$axios.defaults.headers.common['Authorization'] = 'token' +localStorage.getItem("token");
                        //this.$router.push({name:"admin"})
                        //var xx = localStorage.getItem("token")
                       // console.log();
                        this.showingAddModal = false
                        this.init();
                    }
                    else {
                        this.$iziToast.error({
                            title: 'Error',
                            message: "\n" +
                                "The user already exists.",
                        });
                    }
                })
            },
            updateUser(){
                let userToken= 'Bearer' +" " +localStorage.getItem("token");
                // console.log(this.reg);
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.put("http://localhost:56750/Api/Customer/", this.clickedUser, {headers:{ 'Authorization': userToken}}).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.success){
                        this.$axios.defaults.headers.common['Authorization'] = 'token' +localStorage.getItem("token");
                        //this.$router.push({name:"admin"})
                        //var xx = localStorage.getItem("token")
                       // console.log();
                        this.showingEditModal = false
                        this.init();
                    }
                    else {
                        this.$iziToast.error({
                            title: 'Error',
                            message: "\n" +
                                "",
                        });
                    }
                })
            },

            DeleteUser()
            {
                let userToken= 'Bearer' +" " +localStorage.getItem("token");
                let deleteUrl = 'http://localhost:56750/Api/Customer/' + this.clickedUser.id
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.delete(deleteUrl, {headers:{ 'Authorization': userToken}} ).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    //console.log();
                    if(res.data.success){
                        this.$axios.defaults.headers.common['Authorization'] = 'token' +localStorage.getItem("token");
                        //this.$router.push({name:"admin"})
                        //var xx = localStorage.getItem("token")
                        this.showingDeleteModal = false
                        this.init();
                    }
                    else {

                        this.$iziToast.error({
                            title: 'Error',
                            message: "\n" +
                                "",
                        });
                    }
                })
            },
        },
    }
</script>



