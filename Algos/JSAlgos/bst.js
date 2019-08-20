class BSTNode {
    constructor(value) {
        this.value = value;
        this.left = null;
        this.right = null;
    }
}
class BST {
    constructor() {
        this.root = null;
    }
    add(value, node = this.root) {
        if(this.root == null) {
            this.root = new BSTNode(value);
            return;
        }
        
        // check right
        if(value >= node.value) {
            if(node.right == null) {
                node.right = new BSTNode(value);
                return;
            }
            return this.add(value, node.right);
        }
        else { // check left
            if(node.left == null) {
                node.left = new BSTNode(value);
                return;
            }
            return this.add(value, node.left)
        }
    }
}   

let tre = new BST();
tre.add(5);
tre.add(15);
tre.add(10);
tre.add(1);
