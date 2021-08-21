import axios from 'axios';
import { useQuery } from 'react-query';

const claimsKeys = {
  claim: ['recipes']
}

const config = {
  headers: {
    'X-CSRF': '1'
  }
}

const fetchClaims = async () =>
  axios.get('/api/recipes', config)
    .then((res) => res.data);


function useRecipes() {
  return useQuery(
    claimsKeys.claim,
    async () => fetchClaims(),
    {
      staleTime: Infinity,
      cacheTime: Infinity
    }
  )
}

export { useRecipes as default }