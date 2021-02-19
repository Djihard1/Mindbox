<template>
  <div class="admin">
    <modal modalHeading="By" :cond="showingByModal" @modalClose="showingByModal = false">
      <table>
        <tr>
          <td>ShipmentDate</td>
          <td>:</td>
          <td><input type="date" placeholder="ShipmentDate" v-model="newOrder.shipment_data"></td>
        </tr>
        <td>
          <button class="btnSave" @click="toOrder(), this.showingMyordersModal = false"> Buy</button>
        </td>
      </table>
    </modal>

    ///////////////////////

    <modal modalHeading="By" :cond="showingMyordersModal" @modalClose="showingMyordersModal = false">
      <table>
        <table class="nice-table">
          <tr>
            <th>Order Date</th>
            <th>Shipment Date</th>
            <th>Order Number</th>
            <th>Status</th>
          </tr>
          <tr v-for="order in orders">
            <td>{{order.ordeR_DATE}}</td>
            <td>{{order.shipmenT_DATE}}</td>
            <td>{{order.ordeR_NUMBER}}</td>
            <td>{{order.status}}</td>
            <td>
              <button class="delete" @click=" showingEditModal = false , clickedOrder = order,  DeleteOrder()">Delete
              </button>
            </td>
          </tr>
        </table>
      </table>
    </modal>
    <div id="header">
      <h1 class="fleft"> Shop </h1>
    </div>
    <div id="shop">
      <div id="shopContainer">
        <router-view/>
      </div>
    </div>
    <div id="cart">
      <div id="cartContainer">
        <h2>You Cart</h2>
        <hr>
        <table class="cart">
          <tr>
            <th>Product</th>
            <th>Quantity</th>
            <th>Price</th>
          </tr>
          <tr v-for="item in cart">
            <td>{{item.product.name}}</td>
            <td><input type="number" class="input-number " v-model="item.quality"></td>
            <td>{{item.product.price * item.quality}}</td>
          </tr>
          <tr>
            <td class="3">
              <hr>
            </td>
          </tr>
          <tr>
            <td></td>
            <td>Total</td>
            <td>{{total}}</td>
          </tr>
        </table>
        <br><br>
        <p class="txt-center">
          <button class="addBtn" @click="showingByModal = true">Buy</button>
        </p>
        <br>
        <p class="txt-center">
          <button class="addBtn" @click="showingMyordersModal = true, getMyOrders()">My Orders</button>
        </p>
      </div>
    </div>
  </div>
</template>

<script>
    export default {
        name: 'Shop',
        data() {
            return {
                clickedOrder: {},
                cart: [],
                orders: [],
                showingByModal: false,
                showingMyordersModal: false,
                newOrder: {
                    productId: "",
                    customerId: "",
                    item_count: "",
                    shipment_data: "",
                }
            }
            },
        computed: {
            total() {
                var total = 0;
                for (var i = 0; i < this.cart.length; i++) {
                    var item = this.cart[i]
                    total += item.quality * item.product.price
                }
                return total;
            }
        },
        mounted() {
            var token = localStorage.getItem("token");
            if (!token) {
                this.$router.push({name: "login"})
            }
            this.$eventBus.$on("addToCart", payload => {
                this.cart.push(payload)
            })
          //  console.log(this.cart)
        },
        methods: {
            toOrder() {
                // this.newOrder.customerId = localStorage.getItem("customerId")
                this.newOrder.customerId = localStorage.getItem("customerid")
                for (var i = 0; i < this.cart.length; i++) {
                    let item = this.cart[i]
                    let count = Number(item.quality)
                    this.newOrder.productId = item.product.id;
                    this.newOrder.item_count = count;
                    this.$eventBus.$emit("loadingStatus", true);
                    this.$axios.post("http://localhost:56750/api/order/", this.newOrder).then(res => {
                        this.$eventBus.$emit("loadingStatus", false)
                        if (res.data.success) {
                            localStorage.setItem("token", res.data.data);
                            this.$axios.defaults.headers.common['Authorization'] = 'token' + localStorage.getItem("token");
                           //this.$router.push({name: "admin"})
                        }
                        else {
                            this.$iziToast.error({
                                title: 'Error',
                                message: "\n" +
                                    "Error",
                            });
                        }
                    })
                }
                this.showingByModal = false
            },
            getMyOrders() {
                let userToken= 'Bearer' +" " +localStorage.getItem("token");
                console.log(userToken)
                this.$eventBus.$emit("loadingStatus", true);
                let userid = localStorage.getItem("customerid");
                let url = "http://localhost:56750/api/Order/" + userid
                this.$axios.get(url, {headers:{ 'Authorization': userToken}}).then(res => {
                    this.$eventBus.$emit("loadingStatus", false)
                    if (res.data.error) {
                        this.$iziToast.error({
                            title: 'Error',
                            message: res.data.message,
                        });
                    } else {
                        this.orders = res.data.data;
                        console.log(this.orders)
                    }
                })

            },
            DeleteOrder() {
                let deleteUrl = 'http://localhost:56750/api/Order/' + this.clickedOrder.id
                this.$axios.delete(deleteUrl).then(res => {
                })
                this.showingMyordersModal = false
            },
        }
    }
</script>



