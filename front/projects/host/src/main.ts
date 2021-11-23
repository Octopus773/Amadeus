import { loadRemoteEntry } from '@angular-architects/module-federation';

Promise.all([
	loadRemoteEntry('http://localhost:4201/remoteEntry.js', 'weather')
])
	.catch(err => console.error('Error loading remote entries', err))
	.then(() => import('./bootstrap'))
	.catch(err => console.error(err));


// import("./bootstrap")
	// .catch(err => console.error(err));
