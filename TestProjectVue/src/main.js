import { createApp } from 'vue'

import router from './scripts/router.js'
import store from './scripts/store.js'

import App from './App'



var Application= createApp(App)


Application.use(router).use(store).mount('#app')
