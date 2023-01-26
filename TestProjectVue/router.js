import { createRouter, createWebHistory } from 'vue-router'
import SignIn from './components/SignIn.vue'
import SignUp from './components/SignUp.vue'
import Main from './components/Main.vue'
import Issue from './components/IssueForm.vue'
import IssueList from './components/ListIssue.vue'
import IssueReport from './components/IssueReport.vue'
import { store } from './store'

const routes = [
    { path: '/SignIn', name: 'SignIn', component: SignIn, meta: { authorize: false } },
    { path: '/SignUp', name: 'SignUp', component: SignUp, meta: { authorize: false } },
    { path: '/Issue', name: 'Issue', component: Issue, meta: { authorize: true} },
    { path: '/Main', name: 'Main', component: Main, meta: { authorize: true } },
    { path: '/Issues', name: 'Issues', component: IssueList, meta: { authorize: true} },
    { path: '/Issue', name: 'Issue', component: Issue, meta: { authorize: true } },
    { path: '/Report', name: 'Report', component: IssueReport, meta: { authorize: true } },
    {

        path: '/',
        redirect: () => {
          return { path: '/Main' }
        },
      },
]

const router = createRouter({
  // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
  history: createWebHistory(process.env.BASE_URL),
  routes, // short for `routes: routes`
})
router.beforeEach((to, from, next) => {
  store.checkOnAuthorization();
  if (to.meta.authorize && !store.isAuthenticated) next({ path: 'SignIn' });
  else {
    if (to.name == "SignIn" && store.isAuthenticated) {
      next({ path: 'Main' });
    } else next();
  }

  // const { authorize } = to.meta;
  // const currentUser = authenticationService.currentUserValue;
})

export default router