import { createRouter, createWebHistory } from 'vue-router'
import CommetReport from '../Pages/CommetReport.vue'

const routes = [
  { path: '/CommetReport', name: 'CommetReport', component: CommetReport },
  {
    path: '/',
    component: CommetReport,
    redirect: () => {
      return { path: '/CommetReport' }
    },
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router