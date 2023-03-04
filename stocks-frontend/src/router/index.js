import { createRouter, createWebHistory } from 'vue-router'
import StocksView from '../views/Stocks/StocksView.vue'
import AboutView from '../views/Misc/AboutView.vue'
import NotFoundView from '../views/Errors/NotFoundView.vue'

const routes = [
    {
        path: '/',
        name: 'stocks',
        component: StocksView
    },
    {
        path: '/about',
        name: 'about',
        component: AboutView
    },

    // Error page => 404
    {
        path: '/:catchAll(.*)',
        name: 'notFound',
        component: NotFoundView
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router
