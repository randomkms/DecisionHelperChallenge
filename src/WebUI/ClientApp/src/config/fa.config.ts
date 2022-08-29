import { library } from '@fortawesome/fontawesome-svg-core';
import {
  faTree
} from '@fortawesome/free-solid-svg-icons';
import {
  faGithub
} from '@fortawesome/free-brands-svg-icons';

export default function registerIcons(): void {
  library.add(
    faTree,
    faGithub
  );
}
