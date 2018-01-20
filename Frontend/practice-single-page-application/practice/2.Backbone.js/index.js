'use strict';
/* globals Backbone*/

$(() => {
  var Memo = Backbone.Model.extend({
    default: {
      id: 'm00',
      title: '無題',
      content: ''
    }
  });

  var memo1 = new Memo({
    id: 'm01',
    title: 'メモ1',
    content: 'あいうえお'
  });
  console.log(memo1.toJSON());

  memo1.on('change', (m) => {
    var msg = m.get('title') + 'が変更されました。';
    console.log(msg);
    console.log(m.toJSON());
  });

  memo1.set({content: 'アイウエオ'});
  console.log(memo1.get('content'));

  var memo2 = new Memo({
    id: 'm02'
  });
  var memo3 = new Memo({
    id: 'm03'
  });

  var Memos = Backbone.Collection.extend({
    localStorage: new Backbone.LocalStorage('Memos'),
    model: Memo
  });

  var memos = new Memos([memo1, memo2]);
  console.log(memos.length);

  memos.on('add', (m) => {
    console.log(m.get('id') + 'が追加されました。');
  });
  memos.on('remove', (m) => {
    console.log(m.get('id') + 'が削除されました。');
  });

  memos.add(memo3);
  console.log(memos.length);

  memos.remove('m03');
  console.log(memos.length);

  memos.each((m) => {
    m.save();
  });
});
