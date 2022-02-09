import Employee from '../views/Employee.vue'
import VueRouter from 'vue-router';
import AccountObjectList from '../views/account_object/AccountObjectList.vue'
import CashPage from '../views/CashPage.vue'
import Home from '../views/Home.vue'
import CAPayment from '../views/ca_payment/CAPayment.vue'


const routes = [
    { path: "/", redirect: '/home' },
    {
        path: "/home", 
        name: "home", 
        component: Home
    },
    {
        path: "/CAProcess",
        component: CashPage
    },
    {
        path: "/CAProcess/employees",
        component: Employee
    },
    {
        path: "/CAProcess/accountObjs",
        component: AccountObjectList
    },
    {
        path: "/CAProcess/CAPayment",
        component: CAPayment

    },
]

const router = new VueRouter({
    mode: 'history',
    routes: routes
})

export default router;