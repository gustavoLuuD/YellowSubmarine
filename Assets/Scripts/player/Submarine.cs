using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour {
    public bool invencible = false;
    public int level = 1;
    public int hp = 100;
    public int score = 0;
    public int hitDmg = 5;

    private bool moveUp;
    private bool moveDown;
    private bool moveLeft;
    private bool moveRight;
    private bool speedUp;
    private float moveSpeed = 5;

    private Gun[] guns;

    void Start() {
        guns = transform.GetComponentsInChildren<Gun>();
    }

    void Update() {
        //checagem de movimentação
        this.moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        this.moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        this.moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        this.moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        this.speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        //chcagem de tiro
    
        if(Input.GetKey(KeyCode.Space)){
            guns[0].Shoot();
            if(level > 2){
                guns[1].Shoot();
                guns[2].Shoot();
                if(level > 3){
                    guns[3].Shoot();
                    guns[4].Shoot();
                }
            }
            
        }
        else if(Input.GetKey(KeyCode.C))
            guns[5].Shoot();
    }

    private void FixedUpdate() {
        gameOver();
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * Time.fixedDeltaTime * 2;

        if (speedUp) {
            moveAmount *= 2;
        }

        Vector2 move = Vector2.zero;

        if (moveUp) {
            move.y += moveAmount;
        }

        if (moveDown) {
            move.y -= moveAmount;
        }

        if (moveLeft) {
            move.x -= moveAmount;
        }

        if (moveRight) {
            move.x += moveAmount;
        }

        //equalizando a velocidade diagonal com a dos outros eixos
        float moveMagnitude = Mathf.Sqrt((move.x * move.x) + (move.y * move.y));

        if (moveMagnitude > moveAmount) {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        pos += move;

        if (pos.x <= -10.7) {
            pos.x = -10.7f;
        }

        if (pos.x >= 7f) {
            pos.x = 7f;
        }

        if (pos.y <= -3.43f) {
            pos.y = -3.43f;
        }

        if (pos.y >= 3.660f) {
            pos.y = 3.66f;
        }

        transform.position = pos;
    }
    //colisão
    private void OnTriggerEnter2D(Collider2D collision){
      Bullet bullet = collision.GetComponent<Bullet>();
      if(bullet != null){
        if(bullet.isEnemy){
            takeDmg(bullet.hitDmg);
            Destroy(bullet.gameObject);
        }
      }
      Destructable destructable = collision.GetComponent<Destructable>();
      if(destructable != null){
          takeDmg(destructable.hitDmg);
        //Destroy(destructable.gameObject);
      }
    }

    private void gameOver(){
        if(!invencible && hp <= 0)
            Destroy(gameObject);
    }

    private void takeDmg(int dmg){
        hp -= dmg;
    }
}