# SDRSharpPlugins

SDR# �̃v���O�C���W

## SDRSharp.Dial

Arduino�Ń_�C��������ĉ񂷂��  
Arduino���� [������](https://github.com/M-nohira/SDR-Dial)����ǂ����B  
5000�~�n���č���Ă�������B  
![�X�N�V��](https://gyazo.ingen084.net/data/5f7c87302166c7cb96060ab0a99bd331.png)  
�_�C������4��  
�ݒ��ۑ�����@�\�͖������ł��B

`sdrsharp-x86/SDRSharpPlugins` ���ɓW�J����邱�Ƃ�z�肵�Ă��܂��B

### �ʐM�K�i

COM�|�[�g�g�p  
�{�[���[�g: `4800`

#### ��]��M

`0b_XXXX_0BAA`

- `A` 2bit��0�`3�̃_�C�����̔ԍ�
- `B` 1�Ŏ��v���
- `0` �͖��g�p
- `X` �͑O�q4bit�𔽓]�������� �`�F�b�N�p

#### LED�_�������

`0b_XXXX_4321`

- `1`~`4` ���C�g���_�����Ă邩�̃t���O
  - �z��Ƃ��Ă̓_�C�����Ɠ���4�� `0` ����
- `X` �O�q4bit�𔽓]��������

�ڑ�����1�b�����SLED���_�����܂��B

